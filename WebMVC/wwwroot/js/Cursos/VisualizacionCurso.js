// ----------------------
// TABS
// ----------------------
document.querySelectorAll(".tab-btn").forEach(btn => {
    btn.addEventListener("click", () => {
        document.querySelectorAll(".tab-btn").forEach(b => b.classList.remove("active"));
        btn.classList.add("active");

        document.querySelectorAll(".tab-content").forEach(c => c.classList.remove("show"));
        document.getElementById(btn.dataset.target).classList.add("show");
    });
});

// ----------------------
// SEMANAS (MATERIALES)
// ----------------------
document.querySelectorAll(".semana-btn").forEach(btn => {
    btn.addEventListener("click", () => {
        document.querySelectorAll(".semana-btn").forEach(b => b.classList.remove("active"));
        btn.classList.add("active");

        document.querySelectorAll(".semana-material").forEach(s => s.style.display = "none");

        document.getElementById("semana-" + btn.dataset.semana).style.display = "block";
    });
});

// Activar semana 1 por defecto si existe
const primera = document.querySelector(".semana-btn");
if (primera) primera.click();

// ----------------------
// FORO MENSAJES
// ----------------------
function EnviarForo() {
    let input = document.getElementById("foroInput");
    let box = document.getElementById("foroMensajes");

    if (input.value.trim() === "") return;

    let msg = document.createElement("div");
    msg.className = "msg mine";
    msg.textContent = input.value;

    box.appendChild(msg);
    box.scrollTop = box.scrollHeight;
    input.value = "";
}

// ----------------------
// CONTACTO MENSAJES
// ----------------------
function EnviarContacto() {
    let input = document.getElementById("contactoInput");
    let box = document.getElementById("contactoMensajes");

    if (input.value.trim() === "") return;

    let msg = document.createElement("div");
    msg.className = "msg mine";
    msg.textContent = input.value;

    box.appendChild(msg);
    box.scrollTop = box.scrollHeight;
    input.value = "";
}

// ----------------------
// CALENDARIO
// ----------------------
let fecha = new Date();

function DibujarCalendario() {
    const grid = document.getElementById("calGrid");
    const titulo = document.getElementById("mesActual");

    grid.innerHTML = "";

    const mes = fecha.getMonth();
    const ano = fecha.getFullYear();

    titulo.textContent = fecha.toLocaleString("es", { month: "long", year: "numeric" });

    let primerDia = new Date(ano, mes, 1).getDay();
    if (primerDia === 0) primerDia = 7;

    for (let i = 1; i < primerDia; i++) {
        grid.innerHTML += `<div></div>`;
    }

    const diasMes = new Date(ano, mes + 1, 0).getDate();

    for (let d = 1; d <= diasMes; d++) {
        grid.innerHTML += `<div class="calendar-day">${d}</div>`;
    }
}

document.getElementById("prevMes").onclick = () => {
    fecha.setMonth(fecha.getMonth() - 1);
    DibujarCalendario();
};

document.getElementById("nextMes").onclick = () => {
    fecha.setMonth(fecha.getMonth() + 1);
    DibujarCalendario();
};

DibujarCalendario();

document.addEventListener("DOMContentLoaded", function () {

    const hoy = new Date();
    const botones = document.querySelectorAll(".semana-btn");
    const bloques = document.querySelectorAll(".semana-material");

    let marcada = false;

    // Ocultar todos los bloques de materiales
    bloques.forEach(b => b.style.display = "none");

    botones.forEach(btn => {

        const inicio = new Date(btn.dataset.fechainicio);
        const fin = new Date(inicio);
        fin.setDate(fin.getDate() + 7);

        // Verificar si hoy cae dentro de la semana
        if (!marcada && hoy >= inicio && hoy < fin) {

            btn.classList.add("active"); // Resaltado visual
            document.querySelector("#semana-" + btn.dataset.semana).style.display = "block";
            marcada = true;
        } else {
            btn.classList.remove("active");
        }
    });

    // Si no encontró ninguna semana actual, marcar la última
    if (!marcada && botones.length > 0) {
        const ultima = botones[botones.length - 1];
        ultima.classList.add("active");
        document.querySelector("#semana-" + ultima.dataset.semana).style.display = "block";
    }

});    

document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll(".material-link").forEach(item => {
        item.addEventListener("click", () => {

            const tipo = parseInt(item.dataset.tipo);
            const ruta = item.dataset.ruta;
            const texto = item.dataset.texto;

            switch (tipo) {

                case 2:
                    window.open(ruta, "_blank");
                    break;

                case 3:
                case 4: 
                    window.location.href = ruta;
                    break;

                case 5:
                    window.open(ruta, "_blank");
                    break;

                case 6:
                case 1: 
                    mostrarTexto(item, texto);
                    break;

                default:
                    alert("Tipo de material no reconocido.");
            }
        });
    });

});

function mostrarTexto(item, texto) {
    const semanaContainer = item.closest(".semana-material");
    const contenedor = semanaContainer.querySelector(".material-texto");
    contenedor.innerHTML = `<p class="p-2 rounded bg-light">${texto}</p>`;
    contenedor.style.display = "block";
}

document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".agregar-material-btn").forEach(btn => {
        btn.addEventListener("click", () => {
            const semana = btn.dataset.semana;

            document.getElementById("SemanaNumero").value = semana;

            const modal = new bootstrap.Modal(document.getElementById("modalAgregarMaterial"));
            modal.show();
        });
    });
});
document.addEventListener("DOMContentLoaded", () => {

    const modalElement = document.getElementById("modalAgregarMaterial");
    const form = document.getElementById("formAgregarMaterial");
    const inputNombre = form.querySelector("input[name='Nombre']");
    const inputArchivo = form.querySelector("input[name='Archivo']");

    document.querySelectorAll(".agregar-material-btn").forEach(btn => {
        btn.addEventListener("click", () => {

            inputNombre.value = "";
            inputArchivo.value = "";

            document.getElementById("SemanaNumero").value = btn.dataset.semana;

            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        });
    });

    modalElement.addEventListener("hidden.bs.modal", () => {
        inputNombre.value = "";
        inputArchivo.value = "";
    });
});

document.addEventListener('hidden.bs.modal', function () {
    document.querySelectorAll('.modal-backdrop').forEach(b => b.remove());

    document.body.classList.remove('modal-open');
    document.body.style.overflow = "auto";
});