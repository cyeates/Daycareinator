
var AddUser = function(){
	$("#send").click(function () {
		var emailAddress = $("input[name='emailAddress']").val();
		$.post("Users/SendInvite", { emailAddress: emailAddress }, function (result) {
		    notification.show(result);
		});
	});
};