
function showalert(message) {
    alert(message)
}

function DestroyDatatatable() {

    $('#example').DataTable().clear().destroy();
}

function RemoveEmptytable()
{ 
    window.addEventListener('load', function () {
        var tds = document.querySelectorAll('table tr td:first-child:empty')
        tds.forEach(function (td) {
            td.parentNode.setAttribute('hidden', 'hidden')
        })
    })

}