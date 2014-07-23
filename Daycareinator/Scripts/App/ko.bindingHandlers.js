ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        var value = valueAccessor();
        
        var test = options.initialDate;
        $(element).datepicker(options).on("changeDate", function (ev) {
            value(ev.date);
            $(this).datepicker("hide");
        });
                     

        $(".datepicker > .add-on").click(function () {
            $(element).datepicker("show");
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        var date = new Date(value);
        var picker = $(element).datepicker().data("datepicker");
        picker.setDate(date);
        picker.setValue();
        picker.update();
       
    }
};

ko.bindingHandlers.numeralFormat = {
    update: function(element, valueAccessor, allBindingsAccessor){
        var format = allBindingsAccessor().format || '0,0';
        var observable = valueAccessor();
        var value = observable();

        var formattedValue = numeral(value).format(format);
        observable(formattedValue);
        $(element).val(formattedValue);
    },
    //update: function (element, valueAccessor) {
    //    var observable = valueAccessor();
    //    if (!observable.isValid) {
    //        $(element).addClass("error");
    //    }
    //    else {
    //        $(element).removeClass("error");
    //    }
    //}
};

ko.bindingHandlers.maskedInput = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var mask = allBindingsAccessor().mask;
        $(element).mask(mask);
    }
};

ko.bindingHandlers.fileUpload = {
    init: function (element, valueAccessor) {
        $(element).after('<div class="progress"><div class="bar"></div><div class="percent">0%</div></div><div class="progressError"></div>');
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = ko.utils.unwrapObservable(valueAccessor()),
            property = ko.utils.unwrapObservable(options.property),
            url = ko.utils.unwrapObservable(options.url);

        if (property && url) {
            $(element).change(function () {
                if (element.files.length) {
                    var $this = $(this),
                        fileName = $this.val();

                    // this uses jquery.form.js plugin
                    $(element.form).ajaxSubmit({
                        url: url,
                        type: "POST",
                        dataType: "text",
                        headers: { "Content-Disposition": "attachment; filename=" + fileName },
                        beforeSubmit: function () {
                            $(".progress").show();
                            $(".progressError").hide();
                            $(".bar").width("0%")
                            $(".percent").html("0%");
                        },
                        uploadProgress: function (event, position, total, percentComplete) {
                            var percentVal = percentComplete + "%";
                            $(".bar").width(percentVal)
                            $(".percent").html(percentVal);
                        },
                        success: function (data) {
                            $(".progress").hide();
                            $(".progressError").hide();
                            // set viewModel property to filename
                            bindingContext.$data[property](data);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            $(".progress").hide();
                            $("div.progressError").html(jqXHR.responseText);
                        }
                    });
                }
            });
        }
    }
}