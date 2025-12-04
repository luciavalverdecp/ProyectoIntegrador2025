const temaInput = document.getElementById("temaInput");
const agregarTemaBtn = document.getElementById("agregarTema");
const listaTemas = document.getElementById("listaTemas");
const temariosHidden = document.getElementById("temariosHidden");

const difButtons = document.querySelectorAll(".dif-btn");
const difHidden = document.getElementById("dificultad");

// --- Selección de dificultad ---
difButtons.forEach(btn => {
    btn.addEventListener("click", () => {

        // Quitar active de todos
        difButtons.forEach(b => b.classList.remove("active"));

        // Activar el botón clickeado
        btn.classList.add("active");

        // Guardar valor oculto
        difHidden.value = btn.dataset.value;
    });
});

// --- Agregar tema ---
agregarTemaBtn.addEventListener("click", () => {
    const texto = temaInput.value.trim();
    if (texto === "") return;

    const li = document.createElement("li");
    li.innerHTML = `
        ${texto}
        <button type="button" class="eliminar">X</button>
    `;

    listaTemas.appendChild(li);
    temaInput.value = "";

    li.querySelector(".eliminar").addEventListener("click", () => {
        li.remove();
    });
});

// --- Antes de enviar el formulario ---
document.getElementById("cursoForm").addEventListener("submit", function () {

    const contenedor = document.getElementById("temariosContainer");
    contenedor.innerHTML = ""; // limpia por si editaste

    let index = 0;

    document.querySelectorAll("#listaTemas li").forEach(li => {
        const texto = li.childNodes[0].textContent.trim();

        if (texto.length > 0) {

            const input = document.createElement("input");
            input.type = "hidden";
            input.name = `Temarios[${index}].Tema`;
            input.value = texto;

            contenedor.appendChild(input);

            index++;
        }
    });
});

document.getElementById('imagenCurso').addEventListener('change', function (e) {
    if (this.files && this.files[0]) {
        const imgPrev = document.createElement('img');
        imgPrev.src = URL.createObjectURL(this.files[0]);
        imgPrev.style.maxWidth = '200px';
        imgPrev.style.marginTop = '10px';
        this.parentElement.appendChild(imgPrev);
    }
});

document.addEventListener("DOMContentLoaded", function () {

    const fechaInicio = document.getElementById("fechaInicio");
    const fechaFin = document.getElementById("fechaFin");

    const hoy = new Date();
    const yyyy = hoy.getFullYear();
    const mm = String(hoy.getMonth() + 1).padStart(2, "0");
    const dd = String(hoy.getDate()).padStart(2, "0");
    const hoyStr = `${yyyy}-${mm}-${dd}`;

    // Mínimo hoy
    fechaInicio.min = hoyStr;
    fechaFin.min = hoyStr;

    // Deshabilitar fecha fin si no hay inicio
    if (!fechaInicio.value) fechaFin.disabled = true;

    fechaInicio.addEventListener("change", function () {

        if (!fechaInicio.value) {
            fechaFin.disabled = true;
            fechaFin.value = "";
            fechaFin.min = hoyStr;
            return;
        }

        fechaFin.disabled = false;

        // ---- Calcular fecha mínima fin = inicio + 1 mes ----
        const inicio = new Date(fechaInicio.value);
        const minFinDate = new Date(inicio);
        minFinDate.setMonth(minFinDate.getMonth() + 1);

        const yyyyF = minFinDate.getFullYear();
        const mmF = String(minFinDate.getMonth() + 1).padStart(2, "0");
        const ddF = String(minFinDate.getDate()).padStart(2, "0");
        const minFinStr = `${yyyyF}-${mmF}-${ddF}`;

        fechaFin.min = minFinStr;

        // Si fecha fin actual es menor a mínimo ? ajustar
        if (!fechaFin.value || fechaFin.value < minFinStr) {
            fechaFin.value = minFinStr;
        }
    });

});


