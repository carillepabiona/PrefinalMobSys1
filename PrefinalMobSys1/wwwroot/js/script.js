function toggleMenu() {
    const modal = document.getElementById("menuModal");
    modal.style.display = modal.style.display === "block" ? "none" : "block";
}

window.onclick = function (event) {
    const modal = document.getElementById("menuModal");
    if (!event.target.closest('.menu-icon')) {
        modal.style.display = "none";
    }
}
