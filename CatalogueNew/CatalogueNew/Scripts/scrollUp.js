$(document).ready(function () {
    $.scrollUp({
        scrollName: 'scrollUp', // Element ID
        scrollImg: true,
        topDistance: '1000', // Distance from top before showing element (px)
        topSpeed: 500, // Speed back to top (ms)
        animation: 'fade', // Fade, slide, none
        animationInSpeed: 200, // Animation in speed (ms)
        animationOutSpeed: 200, // Animation out speed (ms)
        scrollText: 'Scroll to top', // Text for element
        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
    });
});