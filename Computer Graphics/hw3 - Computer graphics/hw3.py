from helper_classes import *
import matplotlib.pyplot as plt


def render_scene(camera, ambient, lights, objects, screen_size, max_depth):
    width, height = screen_size
    ratio = float(width) / height
    screen = (-1, 1 / ratio, 1, -1 / ratio)  # left, top, right, bottom

    image = np.zeros((height, width, 3))

    for i, y in enumerate(np.linspace(screen[1], screen[3], height)):
        for j, x in enumerate(np.linspace(screen[0], screen[2], width)):
            # screen is on origin
            pixel = np.array([x, y, 0])
            origin = camera
            direction = normalize(pixel - origin)
            ray = Ray(origin, direction)

            # This is the main loop where each pixel color is computed.
            nearest_object, intersection_point = find_intersection(ray, objects)
            color = get_color(ambient, lights, objects, nearest_object, ray, intersection_point, 1, max_depth)
            image[i, j] = color

            # We clip the values between 0 and 1 so all pixel values will make sense.
            image[i, j] = np.clip(color, 0, 1)

    return image


def find_intersection(ray, objects):
    nearest_object, smallest_t = ray.nearest_intersected_object(objects)
    intersection_point = ray.origin + smallest_t * ray.direction
    return nearest_object, intersection_point


def get_color(ambient, lights, objects, nearest_object, ray, intersection_point, level, max_depth):
    if nearest_object is None:
        return np.zeros(3)

    N_Hat = calc_Normal(nearest_object, ray)
    intersection_point += 1e-4 * N_Hat

    color = calc_ambient_color(nearest_object, ambient)
    for light in lights:
        I_L = calc_I_L(light, intersection_point)
        L_normalized = calc_L(light, intersection_point)
        S_j = check_if_not_occluded(objects, intersection_point, light)
        color = color + S_j * (calc_Diffuse_color(nearest_object, I_L, N_Hat, L_normalized) +
                               calc_Specular_color(nearest_object, I_L, N_Hat, L_normalized, ray, intersection_point))
    level += 1
    if level > max_depth:
        return color

    reflective_ray = construct_reflective_ray(ray, intersection_point, N_Hat)
    nearest_object_from_reflection, reflect_intersection_point = find_intersection(reflective_ray, objects)
    if nearest_object_from_reflection is not None:
        N_Hat = calc_Normal(nearest_object_from_reflection, reflective_ray)
        reflect_intersection_point += 1e-4 * N_Hat
        color = color + get_color(ambient, lights, objects, nearest_object_from_reflection, reflective_ray,
                                  reflect_intersection_point, level, max_depth) * nearest_object.reflection

    return color


def check_if_not_occluded(objects, intersection_point, light):
    ray = Ray(intersection_point, light.get_light_ray(intersection_point).direction)
    nearest_object, min_distance = ray.nearest_intersected_object(objects)
    if isinstance(light, PointLight) or isinstance(light, SpotLight):
        if nearest_object is None or min_distance >= light.get_distance_from_light(intersection_point):
            return 1
        else:
            return 0
    elif isinstance(light, DirectionalLight):
        if nearest_object is None:
            return 1
        else:
            return 0


def construct_reflective_ray(ray, intersection_point, N_Hat):
    reflective_ray_direction = normalize(reflected(ray.direction, N_Hat))
    return Ray(intersection_point, reflective_ray_direction)


def calc_I_L(light, intersection_point):
    I_L = light.get_intensity(intersection_point)
    return I_L


def calc_Normal(nearest_object, ray):
    N = None
    if isinstance(nearest_object, Sphere):
        N = calc_Sphere_Normal(nearest_object, ray)
    elif isinstance(nearest_object, Cuboid):
        _, nearest_face, _ = nearest_object.intersect(ray)
        N = nearest_face.normal
    else:
        N = nearest_object.normal
    return N


def calc_Sphere_Normal(sphere, ray: Ray):
    min_t, _ = sphere.intersect(ray)
    intersection_point = ray.origin + min_t * ray.direction
    normal_direction = intersection_point - sphere.center
    return normalize(normal_direction)


def calc_L(light, intersection_point):
    light_ray = light.get_light_ray(intersection_point)
    L = light_ray.direction
    return L


def calc_ambient_color(nearest_object, ambient):
    return nearest_object.ambient * ambient


def calc_Diffuse_color(nearest_object, I_L, N_Hat, L_normalized):
    I_D = nearest_object.diffuse * I_L * np.dot(N_Hat, L_normalized)
    return I_D


def calc_Specular_color(nearest_object, I_L, N_Hat, L_normalized, ray, intersection_point):
    L_Hat = normalize(reflected(L_normalized, N_Hat))
    V = -1 * ray.direction
    I_S = nearest_object.specular * I_L * (np.dot(V, L_Hat) ** nearest_object.shininess)

    return I_S


# Write your own objects and lights
# TODO
def your_own_scene():
    camera = np.array([0, 0, 1])
    background = Plane([0, 0, 1], [0, 0, -3])
    background.set_material([0.2, 0.2, 0.2], [0.2, 0.2, 0.2], [0.2, 0.2, 0.2], 1000, 0.5)

    sphere_a = Sphere([-0.5, 0.2, -1], 0.5)
    sphere_a.set_material([1, 0, 0], [1, 0, 0], [0.3, 0.3, 0.3], 100, 1)

    rectangle = Rectangle([0, 1, -1], [0, -1, -1], [2, -1, -2], [2, 1, -2])
    rectangle.set_material([1, 0, 0], [1, 0, 0], [0, 0, 0], 100, 0.5)

    light_1 = DirectionalLight(intensity=np.array([1, 1, 1]), direction=np.array([1, 1, 1]))
    light_2 = PointLight(intensity=np.array([1, 1, 1]), position=np.array([1, 1, 1]), kc=0.1, kl=0.1, kq=0.1)

    lights = [light_1, light_2]
    objects = [background, sphere_a, rectangle]
    return camera, lights, objects
