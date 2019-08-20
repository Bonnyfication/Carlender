angular.module("umbraco.resources").
    factory("artikelResource", function ($http) {

        return {

            getById: function (id) {

                return $http.get("backoffice/shopSection/ArtikelApi/GetById?id=" + id);

            },

            save: function (artikel) {

                console.log(artikel);

                return $http.post("backoffice/shopSection/ArtikelApi/PostSave", angular.toJson(artikel));

            },

            deleteById: function (id) {

                return $http.delete("backoffice/shopSection/ArtikelApi/DeleteById?id=" + id);

            }

        };

    })