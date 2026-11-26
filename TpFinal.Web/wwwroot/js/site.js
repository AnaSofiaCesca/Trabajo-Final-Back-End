// Fecha con formato dd/mm/aaaa: agrega las barras mientras se escribe
document.querySelectorAll('input[data-mascara-fecha]').forEach(function (input) {
    input.addEventListener('input', function () {
        var numeros = input.value.replace(/\D/g, '').substring(0, 8);
        var resultado = numeros;

        if (numeros.length > 4) {
            resultado = numeros.substring(0, 2) + '/' + numeros.substring(2, 4) + '/' + numeros.substring(4);
        } else if (numeros.length > 2) {
            resultado = numeros.substring(0, 2) + '/' + numeros.substring(2);
        }

        input.value = resultado;
    });
});

// Filtra los doctores según la especialidad elegida
document.querySelectorAll('select[data-filtra-doctores]').forEach(function (selectEspecialidad) {
    var selectDoctor = document.getElementById(selectEspecialidad.dataset.filtraDoctores);

    if (!selectDoctor) {
        return;
    }

    var opcionVacia = selectDoctor.querySelector('option[value="0"]');
    var grupos = Array.from(selectDoctor.querySelectorAll('optgroup'));

    grupos.forEach(function (grupo) {
        selectEspecialidad.add(new Option(grupo.label, grupo.label));
    });

    // Si ya hay un doctor elegido (por ejemplo, al volver con un error), selecciona su especialidad
    var doctorElegido = selectDoctor.selectedOptions[0];

    if (doctorElegido && doctorElegido.parentElement.tagName === 'OPTGROUP') {
        selectEspecialidad.value = doctorElegido.parentElement.label;
    }

    function filtrarDoctores() {
        var especialidad = selectEspecialidad.value;
        var valorAnterior = selectDoctor.value;

        selectDoctor.innerHTML = '';

        if (opcionVacia) {
            selectDoctor.appendChild(opcionVacia);
        }

        grupos.forEach(function (grupo) {
            if (grupo.label === especialidad) {
                selectDoctor.appendChild(grupo);
            }
        });

        selectDoctor.value = valorAnterior;

        if (selectDoctor.value !== valorAnterior) {
            selectDoctor.value = '0';
        }
    }

    filtrarDoctores();
    selectEspecialidad.addEventListener('change', filtrarDoctores);
});
