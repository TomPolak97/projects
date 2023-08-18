"use strict";
const emailSubject = document.querySelector("#subject");
const error = document.querySelector(".error");
let currentPhoto = document.getElementById("tom1");
let imageCollection = [];
imageCollection[0] ="images/Tom1.jpg"
imageCollection[1] = "images/Tom2.jpg";
imageCollection[2] = "images/Tom3.jpg";
imageCollection[3] ="images/Tom4.jpg"
imageCollection[4] = "images/Tom5.jpg";
let idxPhoto = 0;
const navigationslider = () =>
{
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.nav-links');
    const navLinks = document.querySelectorAll('.nav-links li');

    burger.addEventListener('click', () => {
        // Toggle navigation
        nav.classList.toggle('nav-active');

        // opening nav-links when click on the burger
        navLinks.forEach((link,index) => {
            if (link.style.animation)
            {
                link.style.animation = ''
            }
            else
            {
                link.style.animation = 'navLinkFade 0.5s ease forwards ${index/ 7 + 1.5}s';
            }

            console.log(index / 5 + 0.2);
        });
    });
}
navigationslider();

function checkSubjectFromUser() {
    clearSubjectAlert();
    let onlyLetters = /^[a-zA-Z]+$/;
    if (! onlyLetters.test(emailSubject.value))
    {
        error.innerText = "invalid subject, please try again...";
    }
    else
    {
        SendEmail();
    }
}
function clearSubjectAlert()
{
    error.innerText = "";
}
function SendEmail()
{
    const emailOfTom = "tompolak9@gmail.com";
    const emailBody = document.getElementById("message").value;
    window.location.href = "mailto:" + emailOfTom + "?subject=" +emailSubject.value
    +"&body=" + emailBody;
}

function showPictures()
{
    //represent current photo and change to the next one with the "setTimeout".
    currentPhoto.src = imageCollection[idxPhoto];
    if (idxPhoto < imageCollection.length - 1)
    {
        idxPhoto++;
    }
    else
    {
        idxPhoto = 0;
    }
    setTimeout(showPictures,3000);
}
window.onload = showPictures;



