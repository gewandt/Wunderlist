﻿var app = angular.module("WunderlistModule", []);

app.controller('toDoListCtrl', function($scope, $http) {

    $scope.toDoList = "";
    $http.get("/Main/GetToDoLists")
        .success(function(result) {
            $scope.toDoList = result;
        })
        .error(function(result) {
            console.log(result);
        });

    $scope.savedata = function (toDoList) {
        $http.post("/Main/AddToDoList", { name: toDoList.Name })
            .success(function (result) {
                $scope.toDoList = result;
            })
            .error(function(result) {
                console.log(result);
            });
    }
});