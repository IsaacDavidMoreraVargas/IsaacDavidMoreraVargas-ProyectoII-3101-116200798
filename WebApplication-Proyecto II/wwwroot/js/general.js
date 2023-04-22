function primera_vez() {
    evaluar_vacios()
}

function actualizar_fecha(numero, input_meta, input_origen, validar) {
    document.getElementsByClassName(input_meta)[numero].value = document.getElementsByClassName(input_origen)[numero].value;
    /*
    if (validar == true) {
        let fecha_meta = document.getElementsByClassName(input_meta)[0];
        let fecha_origen = document.getElementsByClassName(input_meta)[1];
        try {
            if (Date.parse(fecha_meta.value) < Date.parse(fecha_origen.value)) {
                document.getElementsByClassName(input_meta)[numero].value = document.getElementsByClassName(input_origen)[numero].value;
            } else {
                fecha_origen.value = "";
                alert("Fecha caducidad no puede ser menor o igual a Fecha fabricacion");
            }
        } catch { }
    } else {
        //no asignado
    }
    */
    evaluar_vacios()
}

function evaluar_vacios() {
    let apagar_boton = false;
    let array_inputs = document.getElementsByTagName("input");
    if (array_inputs != null) {
        for (let flag = 0; flag < array_inputs.length; flag++) {
            if (array_inputs[flag].required) {

                let valor = array_inputs[flag].value.replace(" ", "");
                if (valor == "") {
                    array_inputs[flag].style.border = color_error;
                    apagar_boton = true;
                } else {
                    array_inputs[flag].style.border = color_correcto;
                }

            }
        }

    }
    let boton = document.getElementsByClassName("button-to-submit")[0];
    if (boton != null) {
        let color_elegido = "";

        if (apagar_boton == true) {
            color_elegido = color_boton_error;
        } else if (apagar_boton == false) {
            color_elegido = color_boton_correcto;
        }
        //alert(color_elegido);
        boton.disabled = apagar_boton;
        boton.style.backgroundColor = color_elegido;
    }

}
