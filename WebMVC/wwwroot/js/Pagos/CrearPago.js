function abrirModalPago(btn) {
    const estaLogueado = btn.dataset.logueado.toLowerCase() === "true";

    if (!estaLogueado) {
        window.location.href = "/Usuarios/Login";
        return;
    }

    document.getElementById("modalPago").style.display = "flex";
}

function cerrarModalPago() {
    document.getElementById("modalPago").style.display = "none";
}
