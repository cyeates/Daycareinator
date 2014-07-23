var children = function () {

	$(document).on("click", ".open-child", function(){
		var id = $(this).data("id");
		getChild(id);
			
	});

	var getChild = function (id) {
		$.get("api/ChildApi/Get", { childId: id }, function (result) {

		});
	};
};

var childModel = function (data) {

};