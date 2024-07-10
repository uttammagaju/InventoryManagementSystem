/// <reference path="../knockout.js" />
/// <reference path="customer.model.js" />

const mode = {
    creat: 1,
    update: 2
};
var CustomerController = function () {
    var self = this;
    const baseUrl = '/api/CustomerAPI';
    self.IsUpdate = ko.observable(false);
    self.NewCustomer = ko.observable(new CustomerModel());
    self.SelectedCustomer = ko.observableArray([]);
    self.CurrentCustomer = ko.observableArray([]);
    self.mode = ko.observable(mode.create);

    self.GetDatas = function () {
        ajax.get(baseUrl).then(function (result) {
            self.CurrentCustomer(result.map(item = new CustomerModel(item)))
        });
    }

    self.GetDatas();

}


var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
        });
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: data
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
};