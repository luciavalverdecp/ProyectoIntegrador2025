document.addEventListener("DOMContentLoaded", function () {
    const btn = document.getElementById("btnRecuperar");
    const COOLDOWN_KEY = "cooldownRecuperar";
    const COOLDOWN_SEG = 60;

    // Si ya había un cooldown guardado → retomar contador
    let finCooldown = localStorage.getItem(COOLDOWN_KEY);

    if (finCooldown) {
        let tiempoRestante = Math.floor((finCooldown - Date.now()) / 1000);
        if (tiempoRestante > 0) {
            activarCooldown(tiempoRestante);
        } else {
            localStorage.removeItem(COOLDOWN_KEY);
        }
    }

    // Al enviar el formulario → iniciar cooldown
    const form = document.getElementById("formRecuperar");
    form.addEventListener("submit", function () {
        let fin = Date.now() + COOLDOWN_SEG * 1000;
        localStorage.setItem(COOLDOWN_KEY, fin);
    });

    function activarCooldown(segundos) {
        btn.disabled = true;
        const textoOriginal = btn.innerHTML;
        let tiempo = segundos;

        const intervalo = setInterval(() => {
            btn.innerHTML = `Reenviar en ${tiempo}s`;
            tiempo--;

            if (tiempo < 0) {
                clearInterval(intervalo);
                btn.disabled = false;
                btn.innerHTML = textoOriginal;
                localStorage.removeItem(COOLDOWN_KEY);
            }
        }, 1000);
    }
});

document.addEventListener("DOMContentLoaded", function () {

    const vista = document.getElementById("vistaDatosDocente");
    const form = document.getElementById("formEditarDocente");
    const btnEditar = document.getElementById("btnEditarDocente");
    const btnCancelar = document.getElementById("btnCancelarDocente");

    if (btnEditar) {
        btnEditar.addEventListener("click", () => {
            vista.style.display = "none";
            form.style.display = "block";
        });
    }

    if (btnCancelar) {
        btnCancelar.addEventListener("click", () => {
            form.style.display = "none";
            vista.style.display = "block";
        });
    }

});

document.getElementById("btnCancelarDocente").addEventListener("click", () => {
    const form = document.getElementById("formEditar");
form.reset(); // ⬅️ Esto vuelve a los valores originales del HTML

document.getElementById("formEditar").style.display = "none";
document.getElementById("vistaDatos").style.display = "block";
});
