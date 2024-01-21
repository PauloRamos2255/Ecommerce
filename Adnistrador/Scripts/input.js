document.getElementById('txtPrecio').addEventListener('input', function (event) {
    var input = event.target;
    var value = input.value.replace(/[^\d]/g, ''); // Eliminar caracteres que no sean dígitos

    // Asegurar que tenga al menos dos dígitos después del punto
    value = value.padStart(2);

    // Si el valor está vacío o es igual a "0", mostrar "0.00"
    if (value === '' || value === '0') {
        input.value = '0.00';
        input.selectionStart = input.value.length;
        input.selectionEnd = input.value.length;
        return;
    }

    // Insertar el punto después de los dos primeros dígitos
    var integerPart = value.slice(0, -2);
    var decimalPart = value.slice(-2);
    input.value = integerPart + '.' + decimalPart;

    // Colocar el cursor después de los dígitos ingresados por el usuario
    var cursorPosition = integerPart.length;
    input.selectionStart = cursorPosition;
    input.selectionEnd = cursorPosition;
});


