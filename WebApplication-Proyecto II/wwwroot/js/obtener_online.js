﻿function consultaOnline(bandera ,element)
{
    switch (bandera)
    {

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
    }
    
    

}