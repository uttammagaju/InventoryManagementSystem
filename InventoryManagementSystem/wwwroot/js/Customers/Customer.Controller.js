/// <reference path="../knockout.js" />
/// <reference path="customer.model.js" />

const mode = {
    create: 1,
    update: 2
};

var CustomerController = function () {
    var self = this;
    const baseUrl = '/api/CustomerAPI';
    self.IsUpdated = ko.observable(false);
    self.NewCustomer = ko.observable(new CustomerModel());
    self.SelectedCustomer = ko.observableArray([]);
    self.CurrentCustomer = ko.observableArray([]);
    self.mode = ko.observable(mode.create);

    self.GetDatas = function () {
        ajax.get(baseUrl).then(function (result) {
            self.CurrentCustomer(result.map(item => new CustomerModel(item))); // Corrected the mapping function
        });
    }

    self.GetDatas();

    self.AddCustomer = function () {
        var customerData = ko.toJS(self.IsUpdated() ? self.SelectedCustomer : self.NewCustomer);
        switch (self.mode()) {
            case 1:
                ajax.post(baseUrl, JSON.stringify(customerData))
                    .done(function (result) {
                        self.CurrentCustomer.push(new CustomerModel(result));
                        self.GetDatas();
                        self.CloseModel();
                        $('#customerModal').modal('hide');
                    })
                break;
            case 2:
                ajax.put(baseUrl, JSON.stringify(customerData))
                    .done(function (result) {
                        self.CurrentCustomer.replace(self.SelectedCustomer(), new CustomerModel(result));
                        self.CloseModel();
                        self.GetDatas();
                        $('#customerModal').modal('hide');
                    })
        }
    }

    self.DeleteCustomer = function (model) {
        ajax.delete(baseUrl + "?id=" + model.Id())
            .done(function (result) {
                self.CurrentCustomer.remove(result);
                self.GetDatas();
            })
            .fail((err) => {
                console.log(err);
            })
    }

    self.SelectCustomer = function (model) {
        self.SelectedCustomer(model);
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#customerModel').modal('show');
    }

    self.CloseModel = function () {
        self.ResetForm();
        self.GetDatas();
    }

    self.ResetForm = function () {
        self.NewCustomer(new CustomerModel());
        self.SelectedCustomer(new CustomerModel());
        self.IsUpdated(false);
    }
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
            data: (data)
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
}