var color_error = "1px solid #F5524C";
var color_correcto = "1px solid black";

var color_boton_error = "gray";
var color_boton_correcto = "#8fc2ec";

function primera_vez() {
    evaluar_vacios()
}

function actualizar_fecha(numero, input_meta, input_origen, validar) {
    document.getElementsByClassName(input_meta)[numero].value = document.getElementsByClassName(input_origen)[numero].value;
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

function evaluar_float(elemento)
{
    let valor = elemento.value;
    valor = valor.toString();
    if (valor.includes(",")) {
        valor = valor.replace(",", ".");
    }
    if (valor.includes("."))
    {
        let dividido = valor.split('.');
        if (dividido[1].length > 1)
        {
            let unir = dividido[0] + ".";
            let dump = dividido[1];
            for (let flag = 0; flag < 2; flag++)
            {
                unir += dump[flag];
            }
            elemento.value = unir;
        }
    }
    //console.log("here");
}