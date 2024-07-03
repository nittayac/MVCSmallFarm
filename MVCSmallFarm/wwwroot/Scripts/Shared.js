//For detail button in :Shared/Component/ProductAll/ProductAllView.cshtml
function showdetail(id, flg,title,modal) {
    var TeamDetailPostBackURL = '/Product/Detail';
    //$("#btnDetail").on('click', function () {
    //    var $buttonClicked = $(this);
    //    var id = $buttonClicked.attr('data-id');
    var options = { "backdrop": "static", keyboard: true };
    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        data: {
            "Id": id,
            "flg": flg,
            "modal": modal
        },
        datatype: "json",
        success: function (data) {

            if (modal != "nomodal") {
                $('#modalTitle').html(title);
                $('.modal-body').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            } else {
                if (data.success == true) {
                    window.location = data.message;
                }
                // $('#ajaxreponse').html(data);  
            }
            /* alert(data);*/
        },
        error: function () {
            //$('#ajaxreponse').html(data);  
            alert("Dynamic content load failed.");
        }
    });
    /* });*/

    if (modal != "nomodal") {
        $('#ajaxreponse').html('');  
        $(".btn-close").on('click', function () {
            $('#myModal').modal('hide');
        });
    }
}



