angular.module("umbraco").controller("shopSection.editController", function ($scope, $routeParams, artikelResource, notificationsService, navigationService) {

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

    $scope.save = function (artikel) {
        artikelResource.save(artikel).then(function (response) {
            $scope.artikel = response.data;
            notificationsService.success("Success", artikel.Bezeichnung + " " + "has been saved.");
            navigationService.syncTree({ tree: 'artikelTree', path: [-1, $scope.id], forceReload: true }).then(function
                (syncArgs) {

                navigationService.reloadNode(syncArgs.node);
            });

        });
    }

});
