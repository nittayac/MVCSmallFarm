



function onBlazorReady() {

    $('#example').show();
    $('#example').DataTable({
        //"oLanguage": {
        //    "sEmptyTable": "My Custom Message On Empty Table"
        //}===Not working,
        "lengthChange": false,
        "searching": false,
        "info": false,
        "ordering": false,
        "stripeClasses": []
        
    });

    //$.extend($.fn.dataTable.ext.classes, {
    //    sStripeEven: '', sStripeOdd: ''
    //});


}

function showalert(message) {
    alert(message)
}


 