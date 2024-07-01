
function onBlazorReady() {

  /*  $(document).ready(function () {*/
        $('#example').DataTable({
            "searching": false,
            "ordering": false,
            "lengthChange": true,
            "processing": true,
            "lengthMenu": [[3, 6, 9, 12, 15], [3, 6, 9, 12, "Max"]],
            "pageLength": 3,
            "language": {
                "infoEmpty": "Please add Keyword first!",
            }
        });
  /*  });*/
    ////new DataTable('#example', {
    ////    lengthChange: false,
    ////    paging: true,
    ////    pageLength: 4
    ////});

    //$('#example').DataTable({
    //    lengthChange: false,
    //    paging: true,
    //    pageLength: 4
    //});
}

function showalert(message) {
    alert(message)
}


 