if (typeof waldencpb === "undefined") { waldencpb = {}; }
(function ($, contact) {
    contact.init = function () {
        $("form").ajaxForm().validate({
            errorClass: "error",
        });

        $("input[type='submit']").click(function () {
            var $form = $("#contact-form");
            if ($form.valid()) {
                
                $.post($form.attr("action"), $form.serialize(), function (result) {
                    notification.show(result);
                });
            }
            return false;
        });
    }
})(jQuery, waldencpb.contact = waldencpb.contact || {});
