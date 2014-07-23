var User = function(user){
	this.UserId = ko.observable(user.UserId);
	this.EmailAddress = ko.observable(user.EmailAddress);
	//this.Name = ko.computed(function(){
	//	return user.LastName + ", " + user.FirstName; 
	//}, this);
}
var Users = function (dialogElement) {
	var dialogElement = dialogElement;
	var users = ko.observableArray();

	var add = function () {
		var emailAddress = $("input[name='emailAddress']").val();

		var userDto =  {
			UserId: 0,
			EmailAddress: emailAddress
		};

		//$.post("Users/SendInvite", { emailAddress: emailAddress }, function (result) {
		//	notification.show(result);
		//});

		$.ajax({
			url: "api/User/Post",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			data: JSON.stringify(userDto),
			type: "POST",
			success: function (result) {
				notification.show(result);
				$(dialogElement).modal("hide");
				
				users.push(new User(userDto));

				
			}
		});
	};

	var getUsers = function () {
		$.getJSON("api/User/Get", function(result){
			for (var i = 0; i < result.length; i++) {
				users.push(new User(result[i]));
			}
		});
	};

	var remove = function (user) {
		alertify.confirm("Are you sure you want to delete " + user.EmailAddress() + "?", function (e) {
			if (e) {
				$.ajax({
					url: "api/User/Delete/" + user.UserId(),
					type: "DELETE",
					success: function (result) {
						notification.show(result);
						if (result.IsValid) {
							users.remove(user);
						}
					}
				});
			}
		});
	};

	getUsers();

	return {
		add: add,
		users: users,
		remove: remove
	};
};