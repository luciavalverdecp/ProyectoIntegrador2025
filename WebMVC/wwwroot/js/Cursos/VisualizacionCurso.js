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
if (primera && !window.location.hash && !document.querySelector(".tab-content.show")) {
    primera.click();
}


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

    document.querySelectorAll(".agregar-material-btn").forEach(btn => {
        btn.addEventListener("click", () => {
            const semana = btn.dataset.semana;

            document.getElementById("SemanaNumero").value = semana;

            const modal = new bootstrap.Modal(document.getElementById("modalAgregarMaterial"));
            modal.show();
        });
    });
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


function mostrarTexto(item, texto) {
    const semanaContainer = item.closest(".semana-material");
    const contenedor = semanaContainer.querySelector(".material-texto");
    contenedor.innerHTML = `<p class="p-2 rounded bg-light">${texto}</p>`;
    contenedor.style.display = "block";
}


document.addEventListener('hidden.bs.modal', function () {
    document.querySelectorAll('.modal-backdrop').forEach(b => b.remove());

    document.body.classList.remove('modal-open');
    document.body.style.overflow = "auto";
});


// -----------------------
// VARIABLES QUE VIENEN DE RAZOR
// -----------------------
const fechaInicioCurso = new Date(`${FECHA_INICIO_CURSO}T00:00:00`);
const fechaFinCurso = new Date(`${FECHA_FIN_CURSO}T00:00:00`);
const nombreCurso = NOMBRE_CURSO;

const calGrid = document.getElementById("calGrid");
const lblMesActual = document.getElementById("mesActual");
const btnNext = document.getElementById("nextMes");
const btnPrev = document.getElementById("prevMes");

let fechaActual = new Date(); // mes mostrado

// Si estás fuera del curso → movete al mes del inicio/fin
(function clampFecha() {
    const inicioMes = new Date(fechaInicioCurso.getFullYear(), fechaInicioCurso.getMonth(), 1);
    const finMes = new Date(fechaFinCurso.getFullYear(), fechaFinCurso.getMonth(), 1);

    if (fechaActual < inicioMes) fechaActual = new Date(inicioMes);
    if (fechaActual > finMes) fechaActual = new Date(finMes);

    // normalizar día a 1
    fechaActual = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1);
})();

const meses = [
    "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
    "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre"
];


// -----------------------
// RENDERIZAR MES
// -----------------------
function renderMes() {
    const mes = fechaActual.getMonth();
    const anio = fechaActual.getFullYear();

    lblMesActual.textContent = `${meses[mes]} ${anio}`;
    calGrid.innerHTML = "";

    const totalDias = new Date(anio, mes + 1, 0).getDate();
    let hayDias = false;

    for (let dia = 1; dia <= totalDias; dia++) {

        const fechaDia = new Date(anio, mes, dia);
        const hoy = new Date();
        hoy.setHours(0, 0, 0, 0);

        const esPasado = fechaDia < hoy;

        // fuera del rango del curso
        if (fechaDia < fechaInicioCurso || fechaDia > fechaFinCurso) continue;

        hayDias = true;

        const cell = document.createElement("div");
        cell.classList.add("day-cell");

        const fechaStr = `${anio}-${String(mes + 1).padStart(2, "0")}-${String(dia).padStart(2, "0")}`;
        cell.dataset.fecha = fechaStr;

        const claseDelDia = CLASES_AGENDADAS.find(c => c.fecha === fechaStr);

        // ===================================================================
        //                        BLOQUE ESTUDIANTE
        // ===================================================================
        if (ROL_USUARIO === "Estudiante") {

            if (!claseDelDia) {
                cell.innerHTML = `
                    <div class="day-number">${dia}</div>
                    <div class="expand-content">
                        <div class="clase-pasada">No hay clase</div>
                    </div>
                `;
            } else {

                const horaClase = claseDelDia.hora;
                const inicioClase = new Date(`${fechaStr}T${horaClase}:00`);

                cell.innerHTML = `
                    <div class="day-number">${dia}</div>
                    <div class="expand-content">
                        <button class="btn-iniciar" disabled>Ingresar a la clase</button>
                        <div class="hora-clase">${horaClase} hs</div>
                    </div>
                `;

                const btnIngresar = cell.querySelector(".btn-iniciar");

                function actualizarEstado() {
                    btnIngresar.disabled = new Date() < inicioClase;
                }

                actualizarEstado();
                setInterval(actualizarEstado, 30000);

            }

            cell.addEventListener("click", () => expandirCelda(cell));
            calGrid.appendChild(cell);
            continue;
        }

        // ===================================================================
        //                        BLOQUE DOCENTE
        // ===================================================================

        // -------- NO HAY CLASE --------
        if (!claseDelDia) {
            if (esPasado) {
                cell.innerHTML = `
                    <div class="day-number">${dia}</div>
                    <div class="expand-content">
                        <div class="clase-pasada">No disponible</div>
                    </div>
                `;
            } else {
                cell.innerHTML = `
                    <div class="day-number">${dia}</div>
                    <div class="expand-content">
                        <input type="time" class="time-input" step="900" />
                        <button class="btn-agendar">Agendar clase</button>
                    </div>
                `;
            }
        }

        // -------- HAY CLASE --------
        else {

            const inicioClase = new Date(`${fechaStr}T${claseDelDia.hora}:00`);

            cell.innerHTML = `
                <div class="day-number">${dia}</div>
                <div class="expand-content">
                    <button class="btn-iniciar">Iniciar clase</button>
                    <div class="hora-clase">${claseDelDia.hora} hs</div>
                </div>
            `;

            const btnIniciar = cell.querySelector(".btn-iniciar");

            function actualizarHabilitacion() {
                const ahora = new Date();
                const diff = inicioClase - ahora;
                const puede = diff <= 15 * 60 * 1000 && diff > -60 * 60 * 1000;
                btnIniciar.classList.toggle("btn-disabled", !puede);
            }

            actualizarHabilitacion();
            setInterval(actualizarHabilitacion, 30000);

        }

        cell.addEventListener("click", (e) => {
            if (e.target.closest(".btn-iniciar") || e.target.closest(".btn-agendar")) return;
            expandirCelda(cell);
        });

        const btnAgendar = cell.querySelector(".btn-agendar");
        if (btnAgendar) {
            btnAgendar.addEventListener("click", (e) => {
                e.stopPropagation();

                const hora = cell.querySelector(".time-input")?.value;
                if (!hora) return;

                const form = document.createElement("form");
                form.method = "POST";
                form.action = "/Cursos/AgregarClase";

                form.innerHTML = `
                    <input type="hidden" name="fecha" value="${fechaStr} ${hora}">
                    <input type="hidden" name="CursoNombre" value="${NOMBRE_CURSO}">
                `;

                document.body.appendChild(form);
                form.submit();
            });
        }

        calGrid.appendChild(cell);
    }

    if (!hayDias) {
        calGrid.innerHTML = `<div class="empty-month">No hay días del curso en este mes</div>`;
    }

    actualizarFlechas();
}



// -----------------------
// EXPANDIR / COLAPSAR CELDAS
// -----------------------
function expandirCelda(celda) {
    document.querySelectorAll(".day-cell.expanded")
        .forEach(c => { if (c !== celda) c.classList.remove("expanded"); });

    celda.classList.add("expanded");
}

document.addEventListener("click", (e) => {
    const abierta = document.querySelector(".day-cell.expanded");
    if (abierta && !abierta.contains(e.target)) abierta.classList.remove("expanded");
});


// -----------------------
// CONTROL DE FLECHAS SEGÚN FECHAS DEL CURSO
// -----------------------
function actualizarFlechas() {
    const mes = fechaActual.getMonth();
    const anio = fechaActual.getFullYear();

    const mesAnteriorFin = new Date(anio, mes, 0);  // último día del mes anterior
    const mesSiguienteInicio = new Date(anio, mes + 1, 1);

    btnPrev.disabled = mesAnteriorFin < fechaInicioCurso;
    btnNext.disabled = mesSiguienteInicio > fechaFinCurso;
}


// -----------------------
// BOTONES SIGUIENTE / ANTERIOR
// -----------------------
btnNext.addEventListener("click", () => {
    if (btnNext.disabled) return;

    fechaActual.setMonth(fechaActual.getMonth() + 1);
    fechaActual.setDate(1);
    renderMes();
});

btnPrev.addEventListener("click", () => {
    if (btnPrev.disabled) return;

    fechaActual.setMonth(fechaActual.getMonth() - 1);
    fechaActual.setDate(1);
    renderMes();
});


// -----------------------
// INICIO
// -----------------------
renderMes();

// ===============================
// CLICK DELEGADO PARA INICIAR CLASE
// ===============================
document.getElementById("calGrid").addEventListener("click", (e) => {
    const btn = e.target.closest(".btn-iniciar");
    if (!btn) return;

    // si está deshabilitado por clase → no entra
    if (btn.disabled) return;

    const cell = btn.closest(".day-cell");
    if (!cell) return;

    const fecha = cell.dataset.fecha;

    window.location.href =
        `/Cursos/ClasesEnVivo?nombreCurso=${NOMBRE_CURSO}`;
});

window.addEventListener("load", () => {
    const hash = window.location.hash.replace("#", "");
    if (!hash) return;

    document.querySelectorAll(".tab-content").forEach(t => t.classList.remove("show"));
    document.querySelectorAll(".tab-btn").forEach(b => b.classList.remove("active"));

    const tab = document.getElementById(hash);
    const btn = document.querySelector(`.tab-btn[data-target="${hash}"]`);

    if (tab && btn) {
        tab.classList.add("show");
        btn.classList.add("active");
    }
});

function scrollForoAlFinal() {
    const foro = document.getElementById("foroMensajes");
    if (!foro) return;

    foro.scrollTop = foro.scrollHeight;
}

function scrollContactoAlFinal() {
    const cont = document.getElementById("chat-box");
    if (!cont) return;

    cont.scrollTop = cont.scrollHeight;
}

document.querySelectorAll(".tab-btn").forEach(btn => {
    btn.addEventListener("click", () => {

        document.querySelectorAll(".tab-btn")
            .forEach(b => b.classList.remove("active"));
        btn.classList.add("active");

        document.querySelectorAll(".tab-content")
            .forEach(c => c.classList.remove("show"));

        const target = document.getElementById(btn.dataset.target);
        target.classList.add("show");

        if (btn.dataset.target === "foro") {
            setTimeout(scrollForoAlFinal, 50);
        } else if (btn.dataset.target === "contacto") {
            setTimeout(scrollContactoAlFinal, 50);
        }
    });
});

window.addEventListener("load", () => {
    const hash = window.location.hash.replace("#", "");
    if (!hash) return;

    document.querySelectorAll(".tab-content").forEach(t => t.classList.remove("show"));
    document.querySelectorAll(".tab-btn").forEach(b => b.classList.remove("active"));

    const tab = document.getElementById(hash);
    const btn = document.querySelector(`.tab-btn[data-target="${hash}"]`);

    if (tab && btn) {
        tab.classList.add("show");
        btn.classList.add("active");

        // 👉 SI ES FORO → SCROLL
        if (hash === "foro") {
            setTimeout(scrollForoAlFinal, 100);
        } else if (hash === "contacto") {
            setTimeout(scrollContactoAlFinal, 50);
        }
    }
});

//CHAT

document.addEventListener("DOMContentLoaded", () => {

    const rol = document.body.dataset.rol;

    // DOCENTE: click en bandeja
    document.querySelectorAll(".conv-item").forEach(item => {
        item.addEventListener("click", () => {
            const convId = item.dataset.conversacionId;
            cargarChat(convId);
        });
    });

    // ESTUDIANTE: solo cargar si YA existe conversación
    if (rol === "Estudiante") {
        const contenedor = document.querySelector(".contacto-container.estudiante");
        const convId = contenedor?.dataset.conversacionId || 0;

        cargarChat(convId); 
    }
});

function cargarChat(conversacionId) {

    let url = `/Mensajes/ObtenerMensajesConversacion?nombreCurso=${encodeURIComponent(nombreCurso)}`;

    if (conversacionId) {
        url += `&conversacionId=${conversacionId}`;
    }

    fetch(url)
        .then(r => r.text())
        .then(html => {
            document.getElementById("chatPanel").innerHTML = html;
        });
}

