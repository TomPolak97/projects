import numpy as np


# This function gets a vector and returns its normalized form.
def normalize(vector):
    return vector / np.linalg.norm(vector)


# This function gets a vector and the normal of the surface it hit
# This function returns the vector that reflects from the surface
def reflected(vector, normal):
    dot_product = np.dot(vector, normal)
    reflected_vector = vector - 2 * dot_product * normal

    return reflected_vector

## Lights


class LightSource:
    def __init__(self, intensity):
        self.intensity = intensity


class DirectionalLight(LightSource):

    def __init__(self, intensity, direction):
        super().__init__(intensity)
        self.direction = normalize(direction)

    # This function returns the ray that that goes from a point to the light source
    def get_light_ray(self, intersection_point):
        return Ray(intersection_point, self.direction)

    # This function returns the distance from a point to the light source
    def get_distance_from_light(self, intersection):
        return np.inf

    # This function returns the light intensity at a point
    def get_intensity(self, intersection):
        return self.intensity


class PointLight(LightSource):
    def __init__(self, intensity, position, kc, kl, kq):
        super().__init__(intensity)
        self.position = np.array(position)
        self.kc = kc
        self.kl = kl
        self.kq = kq

    # This function returns the ray that goes from the light source to a point
    def get_light_ray(self,intersection):
        return Ray(intersection,normalize(self.position - intersection))

    # This function returns the distance from a point to the light source
    def get_distance_from_light(self,intersection):
        return np.linalg.norm(intersection - self.position)

    # This function returns the light intensity at a point
    def get_intensity(self, intersection):
        d = self.get_distance_from_light(intersection)
        return self.intensity / (self.kc + self.kl*d + self.kq * (d**2))


class SpotLight(LightSource):
    def __init__(self, intensity, position, direction, kc, kl, kq):
        super().__init__(intensity)
        self.position = position
        self.direction = normalize(direction)
        self.kc = kc
        self.kl = kl
        self.kq = kq

    # This function returns the ray that that goes from a point to the light source
    def get_light_ray(self, intersection):
        return Ray(intersection, normalize(self.position - intersection))

    def get_distance_from_light(self, intersection):
        return np.linalg.norm(intersection - self.position)

    def get_intensity(self, intersection):
        d = self.get_distance_from_light(intersection)
        f_att_d = self.kc + self.kl * d + self.kq * (d ** 2)
        v = normalize(intersection - self.position)
        v_d = -1 * self.direction
        return self.intensity * np.dot(v, v_d) / f_att_d


class Ray:
    def __init__(self, origin, direction):
        self.origin = origin
        self.direction = direction

    # The function is getting the collection of objects in the scene and looks for the one with minimum distance.
    # The function should return the nearest object and its distance (in two different arguments)
    def nearest_intersected_object(self, objects):
        nearest_object = None
        min_distance = np.inf

        for object in objects:
            intersection_with_obj = object.intersect(self)
            if intersection_with_obj is not None:
                t = intersection_with_obj[0]
                if t < min_distance:
                    nearest_object = object
                    min_distance = t

        return nearest_object, min_distance


class Object3D:
    def set_material(self, ambient, diffuse, specular, shininess, reflection):
        self.ambient = ambient
        self.diffuse = diffuse
        self.specular = specular
        self.shininess = shininess
        self.reflection = reflection


class Plane(Object3D):
    def __init__(self, normal, point):
        self.normal = np.array(normal)
        self.point = np.array(point)

    def intersect(self, ray: Ray):
        v = self.point - ray.origin
        t = (np.dot(v, self.normal) / np.dot(self.normal, ray.direction))
        if t > 0:
            return t, self
        else:
            return None


class Rectangle(Object3D):
    """
        A rectangle is defined by a list of vertices as follows:
        a _ _ _ _ _ _ _ _ d
         |               |  
         |               |  
         |_ _ _ _ _ _ _ _|
        b                 c
        This function gets the vertices and creates a rectangle object
    """
    def __init__(self, a, b, c, d):
        """
            ul -> bl -> br -> ur
        """
        self.abcd = [np.asarray(v) for v in [a, b, c, d]]
        self.normal = self.compute_normal()

    def compute_normal(self):
        v_1 = self.abcd[1] - self.abcd[0]  # ab
        v_2 = self.abcd[3] - self.abcd[0]  # ad
        normal = np.cross(v_1, v_2)
        return normalize(normal)

    # Intersect returns both distance and nearest object.
    # Keep track of both.
    def intersect(self, ray: Ray):
        # Check if the normal of the rectangle and the direction of the ray are parallel.
        # In this case, there is no intersection.
        if np.isclose(np.dot(self.normal, ray.direction), 0):
            return None

        # Check if there is intersection with the plane of the rectangle
        plane_of_rectangle = Plane(self.normal, self.abcd[0])
        intersection = plane_of_rectangle.intersect(ray)
        if intersection is None:
            return None
        else:
            distance_from_intersect, near_obj = intersection
            p = ray.origin + distance_from_intersect * ray.direction
            for i in range(len(self.abcd)):
                p1 = (self.abcd[i] - p)
                p2 = (self.abcd[(i + 1) % 4] - p)
                if np.dot(self.normal, np.cross(p1, p2)) <= 0:
                    return None

            return distance_from_intersect, self


class Cuboid(Object3D):
    def __init__(self, a, b, c, d, e, f):
        """
              g+---------+f
              /|        /|
             / |  E C  / |
           a+--|------+d |
            |Dh+------|B +e
            | /  A    | /
            |/     F  |/
           b+--------+/c
        """
        g = np.asarray(f) + (np.asarray(a) - np.asarray(d))
        h = np.asarray(b) + (np.asarray(e) - np.asarray(c))
        A = Rectangle(a, b, c, d)
        B = Rectangle(d, c, e, f)
        C = Rectangle(f, e, h, g)
        D = Rectangle(g, h, b, a)
        E = Rectangle(g, a, d, f)
        F = Rectangle(b, h, e, c)
        self.face_list = [A, B, C, D, E, F]

    def apply_materials_to_faces(self):
        for t in self.face_list:
            t.set_material(self.ambient, self.diffuse, self.specular, self.shininess, self.reflection)

    # Hint: Intersect returns both distance and nearest object.
    # Keep track of both
    def intersect(self, ray: Ray):
        # this method returns 3 parameters. In case the nearest object is Cuboid,
        # we can access the nearest face (Rectangle object) and get its normal.
        nearest_face, minimal_distance = ray.nearest_intersected_object(self.face_list)
        return minimal_distance, nearest_face, self


class Sphere(Object3D):
    def __init__(self, center, radius: float):
        self.center = center
        self.radius = radius

    def intersect(self, ray: Ray):
        vector = ray.origin - self.center
        # a, b and c are the coefficients for the quadratic equation that for the intersection
        # of the ray with the sphere.
        a = np.dot(ray.direction, ray.direction)
        b = 2 * np.dot(ray.direction, vector)
        c = np.dot(vector, vector) - self.radius**2
        discriminant = b**2 - 4 * a * c

        if discriminant < 0:
            return None
        else:
            t1 = (-b + np.sqrt(discriminant)) / (2 * a)
            t2 = (-b - np.sqrt(discriminant)) / (2 * a)
            if t1 > 0 and t2 > 0:
                return min(t1, t2), self
            elif t1 > 0:
                return t1, self
            elif t2 > 0:
                return t2, self
            else:
                return None

