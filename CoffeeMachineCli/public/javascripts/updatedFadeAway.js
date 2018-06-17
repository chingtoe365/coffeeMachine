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

//(function () {
//    alert(document.getElementById("last-order-updated"));
//    if (document.getElementById("last-order-updated")) {
//        var lastOrderUpdatedTarget = document.getElementById("last-order-updated");
//        alert(lastOrderUpdatedTarget);
//        var fadeEffect = setInterval(function () {
//            if (!lastOrderUpdatedTarget.style.opacity) {
//                lastOrderUpdatedTarget.style.opacity = 1;
//            }
//            if (lastOrderUpdatedTarget.style.opacity > 0) {
//                lastOrderUpdatedTarget.style.opacity -= 0.1;
//            } else {
//                clearInterval(fadeEffect);
//            }
//        }, 200);
//    }
//})();

