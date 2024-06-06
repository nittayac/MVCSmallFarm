//For detail button in :Shared/Component/ProductAll/ProductAllView.cshtml
function showdetail(id, flg,title) {
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
            "flg": flg
        },
        datatype: "json",
        success: function (data) {
            $('#modalTitle').html(title);
            $('.modal-body').html(data);
            $('#myModal').modal(options);
            $('#myModal').modal('show');
           

            /* alert(data);*/
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
    /* });*/


    $(".btn-close").on('click', function () {
        $('#myModal').modal('hide');
    });

}