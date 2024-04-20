﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))

// Funciones para los botones
function loginFunction() {
    console.log("Login clicked");
    // Implementa la función de login aquí
}

function registerFunction() {
    console.log("Register clicked");
    // Implementa la función de registro aquí
}

$(function () {
    $('[data-toggle="popover"]').popover()

})