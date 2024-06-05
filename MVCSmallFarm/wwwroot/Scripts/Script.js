//$(document).on("submit", "form", function (e) {
//    e.preventDefault();
//    alert('it works!');
//    return false;
//});

async function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        //reader.onload = function (e) {
        //    $(previewImage).attr('src', e.target.result);
        //}

        reader.onload = (function (theFile) {
            var image = new Image();
            image.src = theFile.target.result;
            image.onload = function () {
                // access image size here 
                console.log(this.width);
                var ratio = 0;
                var maxWidth = 130;
                var maxHeight = 170;

                var width = $(this).width();
                var height = $(this).height();
                if (width > maxWidth) {
                    ratio = maxWidth / width;
                    $(this).css("width", maxWidth);
                    $(this).css("height", height * ratio);
                    height = height * ratio;
                }
                var width = $(this).width();
                var height = $(this).height();
                if (height > maxHeight) {
                    ratio = maxHeight / height;
                    $(this).css("height", maxHeight);
                    $(this).css("width", width * ratio);
                    width = width * ratio;
                }

                $(previewImage).attr('src', this.src);
            };
        });
       await reader.readAsDataURL(imageUploader.files[0]);
        
    }
}


function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'Post',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    //alert(response.message)
                } else {
                    //alert(response.message)
                };
            },
            error: function (error) {
                // console.error(error);
            }
        }

        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig['contentType'] = false;
            ajaxConfig['processData'] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}


function jQueryAjaxPostByID(id,url) {

    var ajaxConfig = {
        type: 'POST',
        url: url,
        data: { id: id }, 
        success: function (response) {
            if (response.success) {
                //alert(response.message)
            } else {
                //alert(response.message)
            };
        },
        error: function (error) {
            console.error(error);
        }
    }

    $.ajax(ajaxConfig);
    return false;
}



//For detail button in :Shared/Component/ProductAll/ProductAllView.cshtml
function showdetail(id, flg) {
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
            $('.modal-body').html(data);
            $('#myModal').modal(options);
            $('#myModal').modal('show');
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

