
function esconder_tablas_primera_vez()
{
    //alert("here");
    esconder_menu_reporte()
    esconder_tablas_reporte()
}

function esconder_menu_reporte() {
    let elemento_menu_reporte = document.getElementsByClassName("contenedor-columna");
    if (elemento_menu_reporte != null) {
        for (let bandera = 0; bandera < elemento_menu_reporte.length; bandera++) {
            elemento_menu_reporte[bandera].style.display = "block";
        }
    }
}

function esconder_tablas_reporte()
{
    let arrary_tablas = document.getElementsByClassName("tabla-reporte");
    if (arrary_tablas != null) {
        for (let bandera = 0; bandera < arrary_tablas.length; bandera++) {
            arrary_tablas[bandera].style.display = "none";
        }
    }

    try {

        let array = document.getElementsByClassName("regresar-reporte");
        if (array != null) {
            array[0].style.display = "none";
        }
    } catch { }
    
}

function actualizar_tabla_en_uso(numero) {
    let elemento_menu_reporte = document.getElementsByClassName("contenedor-columna");
    if (elemento_menu_reporte != null) {
        for (let bandera = 0; bandera < elemento_menu_reporte.length; bandera++) {
            elemento_menu_reporte[bandera].style.display = "none";
        }
    }

    let elemento_mostrar = document.getElementsByClassName("tabla-reporte");
    if (elemento_mostrar != null) {
        elemento_mostrar[numero].style.display = "block";
    }

    elemento_mostrar = document.getElementsByClassName("regresar-reporte")[0];
    if (elemento_mostrar != null) {
        elemento_mostrar.style.display = "block";
    }
}

function regreso_a_estado_inicio_reporte()
{
    esconder_tablas_primera_vez()
}

function abrir_reporte_SSRS(bandera)
{

    switch (bandera) {
        case 0:
            window.open("https://localhost/ReportServer/Pages/ReportViewer.aspx?%2fpRUEBA%2fReportTodosSobreRetiro&rs:Command=Render", "_blank");
            break;

        case 1:
            window.open("https://localhost/ReportServer/Pages/ReportViewer.aspx?%2fpRUEBA%2fReportRetirosFecha&rs:Command=Render", "_blank");
            break;
    }
}