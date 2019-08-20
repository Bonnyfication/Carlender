angular.module("umbraco").controller("Plugin.ArtikelselektorController", function ($scope, assetsService) {

    $scope.artikelliste = [];


    jQuery.ajax({
        url: '/Handler/getArtikel.ashx',
        dataType: 'json',
        type: 'GET',
        contentType: 'application/json',
        success: function (response) {
            $scope.artikelliste = response;
        },
        error: function (data, status, jqXHR) {
            alert('Error');
        }
    }); // end jQuery.ajax

    $scope.onChange = function () {
        //$scope.model.value.idArtikel = $scope.selectedArtikel;

        var r = $scope.model.value.idArtikel;
    }
});