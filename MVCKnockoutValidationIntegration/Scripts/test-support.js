var addSaveSupport = function (obj) {
    obj.save = function () {
        $.ajax({
            url: "/api/ViewModelServing/CheckSimpleViewModel",
            data: ko.toJSON(obj),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            cache: false,
            processData: false,
            error: function (xhr, status) {
                alert('Invalid');
            },
            success: function (data) {
                alert('Valid');
            }
        });
    };
};