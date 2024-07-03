async function ReloadEventViewComponent() {
    var chk = 0;

    //var urlstr: "@Url.Action("ReloadEventViewComponent", "Product")";
    var urlstr = "Product/ReloadEventViewComponent";    

    var ajaxConfig = {
        type: 'GET',
        url: urlstr,
        data: {
            pgview: 1,
            flg: 1
        },
        success: function (data) {
            //$(document).ready(LoadDatatable);
            $("#tabfirst").html(data);
            var table = $('#tblViewAll');
            TestDataTablesRemove(table);
        }
    }

    $.ajax(ajaxConfig);

    await ResetTab(1);
}



function TestDataTablesRemove(table) {
    $(document).ready(function () {
        //table = $('#tblViewAll');
        // 1stclear first
        //if (table != null) {
        //    $(table).DataTable().clear();
        //    $(table).DataTable().destroy();
        //    //tableObj.clear();
        //    //tableObj.destroy();
        //}

        //2nd empty html
        //$(tableId + " tbody").empty();
        //$(tableId + " thead").empty();

        //3rd reCreate Datatable object
        $(table).DataTable({
            "autoWidth": false,
            ordering: false
        });

    });
}

async function ReloadProductAddEditViewComponent(id, edit) {
    if (edit.toLowerCase() == 'edit') {
        ResetTab(2);
    }


    //var urlstr = @Url.Action("ProductAddEditView", "Product");

    var urlstr = "Product/ProductAddEditView";        ;

    var ajaxConfig = {
        type: 'GET',
        url: urlstr,
        data: {
            pgview: 2,
            flg: id
        },
        success: function (data) {
            $("#tabsecond").html(data);
        }
    }
    await $.ajax(ajaxConfig);

    if (edit.toLowerCase() == 'reset') {
        ResetTab(3);
    }
}

function ResetTab(chk) {
    let el_down = document.getElementById("linkSecond");
    if (chk == 1) {
        //add
        el_down.innerHTML = "Add new";
        document.getElementById("linkFirst").click();
    } else if (chk == 2) {
        //edit
        el_down.innerHTML = "Edit";
        document.getElementById("linkSecond").click();
    } else {
        //reset
        el_down.innerHTML = "Add new";
        document.getElementById("linkFirst").click();
    }
}

function confirmSubmitJQ(id, msg, url, loadmsg) {

    checkfn(function () {
        //alert('success!');
        OpenBusyDialog(1, loadmsg);
        var chk = jQueryAjaxPostByID(id, url);
        setTimeout(
            function () {
                ReloadEventViewComponent();
            }, 2000);
        OpenBusyDialog(0, "");

    }, function () {
        //alert('failure!');
    }, msg);

}

function checkfn(success, cancel, msg) {
    $.confirm({
        title: 'Confirm!',
        content: msg,
        buttons: {
            confirm: function () {
                if (success) success();
                var self = this;
                self.close;
            },
            cancel: function () {
                //  $.alert('Canceled!');
                if (cancel) cancel();
                var self = this;
                self.close;
            }

        }
    });


    return true;
}

function OpenBusyDialog(chk, msg) {
    //var theDialog = $("#gfg").dialog(opt);
    //theDialog.dialog("open");

    if (chk == 1) {
        $("#loadmsg").html(msg);
        $("#gfg").dialog("open");
    } else {
        $("#loadmsg").html();
        $("#gfg").dialog("close");
    }

}


//for a form in Product/ProductAddEdit.cshtml
function jQueryAjaxReset() {

    // var validator = $("#frm").validate(); //Not working
    //validator.resetForm();                //Not working
    //$(form).reset();                      //Not working
    //$(form).removeData("validator").removeData("unobtrusiveValidation");
    ReloadProductAddEditViewComponent(0, "reset");
}


//Using with SubmitForm() for a form in Product/ProductAddEdit.cshtml
function ValidateForm() {
    var isAllValid = true;
    //$('.text-danger').hide();
    //$('#error').empty();  

    var instock = parseInt($('#InStock').val().trim());
    var sold = parseInt($('#SoldTotals').val().trim());

    if (isNaN(sold) == false) {
        if (Number(instock) < Number(sold)) {
            $("#SoldTotals").focus();

            //If you want to put the message in the span, then you have to set data - valmsg - replace to false.
            $("#SoldTotals").siblings('.text-danger').attr("data-valmsg-replace", function (i, val) {
                // return val + " - photo by Kelly Clark";
                return false;
            });

            $("#SoldTotals").siblings('.text-danger').show();
            $("#SoldTotals").siblings('.text-danger').text('Must be less than or equal to an Instock value.');
            $("#SoldTotals").parents('.form-group').addClass('has-error');

            isAllValid = false;
        } else {

            $("#SoldTotals").siblings('.text-danger').hide();
            $("#SoldTotals").siblings('.text-danger').text('');

            //Intitial span
            $("#SoldTotals").siblings('.text-danger').attr("data-valmsg-replace", function (i, val) {
                return true;
            });


            $("#SoldTotals").parents('.form-group').removeClass('has-error');
            isAllValid = true;
        }
    }
    else {
        $("#SoldTotals").siblings('.text-danger').show();
        console.log('else');
        isAllValid = false;
    }
    return isAllValid;
}

// (view componentin tabsecond):Product/ProductAddEdit.cshtml  (A Form calls)
function SubmitForm(form) {

    $.validator.unobtrusive.parse(form);
    console.log("SubmitForm start validate");

    // debugger; 
    var chk = ValidateForm();
    if ($(form).valid() && chk) {

        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            //data: $(form).serialize(),
            //dataType: "html",
            success: function (response) {
                if (response.success) {
                    OpenBusyDialog(1, "      Saving <br/> Please wait.");

                    setTimeout(
                        function () {
                            ReloadProductAddEditViewComponent(0, "reset");  
                            ReloadEventViewComponent();                     
                        }, 2000
                    );

                    OpenBusyDialog(0, "");
                } else {
                    //$.validator.unobtrusive.parse(form);
                    /*$("#tabsecond").html(response.FormData);*/
                    $("#tabsecond").html(response);
                    // $("#imgpreview").attr('src', 'data:image/png,image/jpeg;base64,' + response);
                };
            },
            error: function (error) {
                console.error(error);
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







