// Function to make "updated" remark fade out each time an order is made
window.onload = function () {
    var lastOrderUpdatedTarget = document.getElementById("last-order-updated");
    if (lastOrderUpdatedTarget) {
        var fadeEffect = setInterval(function () {
            if (!lastOrderUpdatedTarget.style.opacity) {
                lastOrderUpdatedTarget.style.opacity = 1;
            }
            if (lastOrderUpdatedTarget.style.opacity > 0) {
                lastOrderUpdatedTarget.style.opacity -= 0.1;
            } else {
                clearInterval(fadeEffect);
            }
        }, 100);
    }
}
