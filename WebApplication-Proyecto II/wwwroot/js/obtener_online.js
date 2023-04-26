function consultaOnline(bandera ,element)
{
    switch (bandera) {

        case 0:
            var url = "/Administrador/ConsultaInmediataCodigoLibro/" + "?id=" + element.value;
            var request = new XMLHttpRequest();
            request.responseType = 'text';

            request.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    if (this.responseText != "null") {
                        let convertido = JSON.parse(this.responseText);
                        document.getElementsByTagName("input")[2].value = convertido.Codigo_Libro;
                        evaluar_vacios()
                    }
                }
            };

            request.open('GET', url, true);
            request.send();
            break;

        case 1:
            var url = "/Administrador/ConsultaInmediataCodigoCliente/" + "?id=" + element.value;
            var request = new XMLHttpRequest();
            request.responseType = 'text';

            request.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    if (this.responseText != "null") {
                        let convertido = JSON.parse(this.responseText);
                        document.getElementsByTagName("input")[5].value = convertido.Codigo_Cliente;
                        evaluar_vacios()
                    }
                }
            };

            request.open('GET', url, true);
            request.send();
            break;

        case 2:

            
                //console.log("here");
                var url = "/Administrador/ConsultaStockDeClienteDisponible/" + "?id=" + element.value;
                var request = new XMLHttpRequest();
                request.responseType = 'text';

                request.onreadystatechange = function () {
                    if (this.readyState == 4 && this.status == 200) {
                        //console.log(this.responseText);
                        if (this.responseText != "null") {
                            let numero = 0;
                            let final_acumulador = "";
                            let convertido = JSON.parse(this.responseText);
                            for (let valor in convertido) {
                                let acumulador = "<div class=*leyenda-tabla*>Libros no retirados</div>";
                                acumulador += "<div class=*fila-tabla-cabeza*><div class=*cabeza-tabla*>Codigo libro</div><div class=*cabeza-tabla*>Descripcion libro</div><div class=*cabeza-tabla*>Precio articulo</div><div class=*cabeza-tabla*>Codigo cliente</div><div class=*cabeza-tabla*>Fecha ingreso</div><div class=*cabeza-tabla*>Opcion</div></div>";
                                acumulador += "<div class=*fila-tabla*><div class=*fila-fila*>";
                                acumulador += "<div class=*valor-tabla*>" + convertido[valor].Codigo_Libro + "</div>";
                                acumulador += "<div class=*valor-tabla*>" + convertido[valor].Descripcion_Articulo + "</div>";
                                acumulador += "<div class=*valor-tabla*>" + convertido[valor].Precio_Articulo + "</div>";
                                acumulador += "<div class=*valor-tabla*>" + convertido[valor].Codigo_Cliente + "</div>";
                                acumulador += "<div class=*valor-tabla*>" + convertido[valor].Fecha_Ingreso + "</div>";
                                acumulador += "<button class=*valor-tabla resaltar si-valor* onclick=*actualizarRetiro(this)* value=*" + convertido[valor].Llave_Libro + "*>Retirar</button>";
                                acumulador += "</div>";
                                acumulador += "<div class=*contenedor-datos-tabla*><label class=*titulo-valor-tabla color-padre*>Fecha Retiro</label><input class=*box-input-valor-tabla border-hijo* readonly type=*text* maxlength=*30* required /><input class=*input-date-valor-tabla* type=*date* onchange=*actualizar_fecha(" + numero + ",'box-input-valor-tabla','input-date-valor-tabla',false)* required /></div>";
                                acumulador += "</div>";
                                
                                //sava = sava.replaceAll('*', '"');
                                //console.log(sava);
                                final_acumulador += acumulador.replaceAll('*', '"');
                                numero++;
                            }
                            document.getElementsByClassName("cuerpo-filas")[0].innerHTML = "";
                            document.getElementsByClassName("cuerpo-filas")[0].innerHTML += final_acumulador;
                            consultaNO(element)
                        } else {
                            let error = "<div class=*leyenda-tabla*>Libros no retirados</div>";
                            error += "<div class=*fila-tabla*><div class=*rechazo-tabla*>Sin registros</div></div> ";
                            error = error.replaceAll('*', '"');
                            document.getElementsByClassName("cuerpo-filas")[0].innerHTML = "";
                            document.getElementsByClassName("cuerpo-filas")[0].innerHTML += error;

                            consultaNO(element)
                        }
                    }
                };

                request.open('GET', url, true);
                request.send();

                
            
            break;
    }
}
function consultaNO(element)
{
    var url = "/Administrador/ConsultaStockDeClienteNoDisponible/" + "?id=" + element.value;
    var request = new XMLHttpRequest();
    request.responseType = 'text';

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText != "null") {
                let final_acumulador = "";
                let convertido = JSON.parse(this.responseText);
                
                for (let valor in convertido) {
                    let acumulador = "<div class=*leyenda-tabla*>Libros retirados anteriormente</div>";
                    acumulador += "<div class=*fila-tabla-cabeza*><div class=*cabeza-tabla*>Codigo libro</div><div class=*cabeza-tabla*>Nombre libro</div><div class=*cabeza-tabla*>Descripcion libro</div><div class=*cabeza-tabla*>Codigo cliente</div><div class=*cabeza-tabla*>Fecha retiro</div><div class=*cabeza-tabla*>Opcion</div></div>";
                    acumulador += "<div class=*fila-tabla*><div class=*fila-fila*>";
                    acumulador += "<div class=*valor-tabla*>" + convertido[valor].Codigo_Libro + "</div>";
                    acumulador += "<div class=*valor-tabla*>" + convertido[valor].Nombre_Libro + "</div>";
                    acumulador += "<div class=*valor-tabla*>" + convertido[valor].Descripcion_Articulo + "</div>";
                    acumulador += "<div class=*valor-tabla*>" + convertido[valor].Codigo_Cliente + "</div>";
                    acumulador += "<div class=*valor-tabla*>" + convertido[valor].Fecha_Retiro + "</div>";
                    acumulador += "<button disabled class=*valor-tabla resaltar no-valor* value=*" + convertido[valor].Llave_Libro + "*>Retirado</button>";
                    acumulador += "</div>";
                    acumulador += "</div>";
                    final_acumulador += acumulador.replaceAll('*', '"');
                  
                }

                document.getElementsByClassName("cuerpo-filas")[0].innerHTML += final_acumulador;
            } else {
                let error = "<div class=*leyenda-tabla*>Libros retirados anteriormente</div>";
                error += "<div class=*fila-tabla*><div class=*rechazo-tabla*>Sin registros</div></div> ";
                error = error.replaceAll('*', '"');
                document.getElementsByClassName("cuerpo-filas")[0].innerHTML += error;
            }
        }
    };

    request.open('GET', url, true);
    request.send();
}

function actualizarRetiro(elemento)
{
    var result = confirm("¿Seguro desea retirar?");
    if (result == true) {
        let elemento_padre = elemento.parentNode;
        elemento_padre = elemento_padre.parentNode;
        //alert(elemento_padre.innerHTML);
        if (elemento_padre.getElementsByClassName("box-input-valor-tabla")[0].value == "")
        {
            alert("Fecha retiro no seleccionada");
        } else
        {
            let valor_envio = elemento.value + "*" + elemento_padre.getElementsByClassName("box-input-valor-tabla")[0].value;
            var url = "/Administrador/ConsultaCambioEstado/" + "?valor=" + valor_envio;
            var request = new XMLHttpRequest();
            request.responseType = 'text';

            request.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    //console.log(this.responseText);
                    if (this.responseText != "null") {
                        elemento.style.backgroundColor = "#E1E4F4";
                        elemento.innerText = "Retirado";
                        elemento.disabled = true;
                        elemento_padre.getElementsByClassName("box-input-valor-tabla")[0].value = "";
                        elemento_padre.getElementsByClassName("box-input-valor-tabla")[0].disabled = true;
                        elemento_padre.getElementsByClassName("input-date-valor-tabla")[0].disabled = true;

                    }
                }
            };

            request.open('GET', url, true);
            request.send();
        }
    } else {
        alert("Operacion abortada por el usuario");
    }
}