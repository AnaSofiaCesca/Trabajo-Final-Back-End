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
