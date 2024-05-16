
function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function(e){
            $(previewImage).attr('src', e.target.result);

        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function jQueryAjaxPost(form) {
   // var form = new FormData(document.forms.item(0));
    //var file = $('#fileUpload').prop("files")[0];
    //form.append('fileUpload', file);

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) { 
        var ajaxConfig =  {
            type: 'Post',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                //$("#frm")[0].reset();    //Not working so reset from in viewcomponent using  ReloadProductAddEditViewComponent(0)
                //$("#imgpreview").attr("src", defaultImg);
                ReloadProductAddEditViewComponent(0);   // Code in View/Product/Index.cshtml
                ReloadEventViewComponent();             // Code in View/Product/Index.cshtml   
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
