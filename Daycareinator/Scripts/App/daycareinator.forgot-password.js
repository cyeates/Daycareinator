var ForgotPassword = function () {
    $("button").click(function (e) {
       
        e.preventDefault();

		var $form = $("form");
		$.post("ForgotPassword", $form.serialize(), function (data) {
		    notification.show(data);
		});
		return false;
		
	});
};