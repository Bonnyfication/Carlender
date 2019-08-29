angular.module("umbraco.resources").
    factory("showResource", function ($http) {
        return {
            getById: function (id) {
                return $http.get("backoffice/showSection/ShowApi/GetById?id=" + id);
            },
            save: function (show) {
                console.log(show);
                return $http.post("backoffice/showSection/ShowApi/PostSave", angular.toJson(show));
            },
            deleteById: function (id) {
                return $http.delete("backoffice/showSection/ShowApi/DeleteById?id=" + id);
            }
        };
    })
