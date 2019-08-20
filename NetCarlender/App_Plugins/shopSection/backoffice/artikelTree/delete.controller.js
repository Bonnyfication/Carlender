angular.module("umbraco").controller("shopSection.deleteController", function ($scope, $routeParams, artikelResource, notificationsService, navigationService) {

    $scope.loaded = false;
    //
    if ($routeParams.id == -1) {
        $scope.artikel = {};
        $scope.loaded = true;
    }
    else {
        artikelResource.getById($routeParams.id).then(function (response) {
            $scope.artikel = response.data;
            $scope.loaded = true;
        });
    }

    $scope.cancelDelete = function() {

        navigationService.hideNavigation();

    }

    $scope.delete = function (artikel) {
        artikelResource.deleteById(artikel).then(function (response) {
            //$scope.artikel = response.data;

            notificationsService.success("Success", "The Artikel has been deleted.");
            navigationService.hideNavigation();
            navigationService.syncTree({ tree: 'artikelTree', path: [-1, $scope.id], forceReload: true }).then(function
                (syncArgs) {
                navigationService.reloadNode(syncArgs.node);
            });


        });
    }

});