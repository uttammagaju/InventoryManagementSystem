/// <reference path="purchases.model.js" />

const mode = {
    create: 1,
    update: 2
}
var PurchasesController = function () {
    var self = this;
    const baseUrl = '/api/PurchaseReportAPI';
    self.NewPurchase = ko.observable(new PurchaseMasterVM({}, self));
    self.CurrentPurchase = ko.observableArray([]);
    self.SelectedPurchase = ko.observable(new PurchaseMasterVM({}, self));
    self.IsUpdated = ko.observable(false);
    self.mode = ko.observable(mode.create);
    self.PurchaseToBeDelete = ko.observable();
    self.VendorNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.venors = ko.observable(new VendorsNameVM());

    self.GetData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            console.log(result);
            self.CurrentPurchase(result.map(item => new PurchaseMasterVM(item, self)));
        });
    }
    self.GetData();

    self.GetVendorNameList = function () {
        ajax.get(baseUrl + "/GetAllVendor").then(function (result) {
            self.VendorNameList(result.map(item => new VendorsNameVM(item)));
        });
    }
    self.GetVendorNameList();

    self.GetItemsName = function () {
        ajax.get(baseUrl + "/GetItemsName").then(function (result) {
            self.ItemsNameList(result.map(item => new ItemsNameVM(item)));
        });
    }
    self.GetItemsName();

    self.AddItem = function () {
        if (self.IsUpdated()) {
            self.SelectedPurchase().Purchases.push(new PurchaseDetailsVM({}, self));
        } else {
            self.NewPurchase().Purchases.push(new PurchaseDetailsVM({}, self));
        }
    }

    self.RemoveItem = function (item) {
        if (self.IsUpdated()) {
            self.SelectedPurchase().Purchases.remove(item);
        } else {
            self.NewPurchase().Purchases.remove(item);
        }
    }

    self.DeletePurchase = function (model) {
        self.PurchaseToBeDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };

    self.AddPurchase = function () {
        var purchaseData = ko.toJS(self.IsUpdated() ? self.SelectedPurchase : self.NewPurchase);
        switch (self.mode()) {
            case mode.create:
                debugger
                ajax.post(baseUrl + "/Create", JSON.stringify(purchaseData))
                    .done(function (result) {
                        console.log("Data received", result);
                        self.CurrentPurchase.push(new PurchaseMasterVM(result, self));
                        self.ResetForm();
                        self.GetData();
                        $('#orderModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log("Error adding order:", err);
                    });
                break;
            case mode.update:
                ajax.put(baseUrl + "/Update", JSON.stringify(purchaseData))
                    .done(function (result) {
                        var updatedOrder = new PurchaseMasterVM(result, self);
                        var index = self.CurrentPurchase().findIndex(function (item) {
                            return item.Id() === updatedOrder.Id();
                        });
                        if (index >= 0) {
                            self.CurrentPurchase.replace(self.CurrentPurchase()[index], updatedOrder);
                        }
                        self.ResetForm();
                        self.GetData();
                        $('#orderModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log("Error updating order:", err);
                    });
                break;
        }
    };

    self.SelectPurchase = function (model) {
        self.SelectedPurchase(model);
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#orderModal').modal('show');
    }

    self.ResetForm = function () {
        self.NewPurchase(new PurchaseMasterVM({}, self));
        self.SelectedPurchase(new PurchaseMasterVM({}, self));
        self.IsUpdated(false);
        self.AddItem();
    }

    self.calculateTotal = function () {
        self.totalAmount.notifySubscribers();  // Recalculate the total amount
    };

    self.CloseModel = function () {
        self.ResetForm();
    }

    self.ResetForm();

    self.confirmDelete = function () {
        var model = self.PurchaseToBeDelete();
        if (model) {
            ajax.delete(baseUrl + "?id=" + model.Id())
                .done((result) => {
                    self.CurrentCustomer.remove(model);
                    $('#deleteConfirmModal').modal('hide');
                })
                .fail((err) => {
                    console.log(err);
                    $('#deleteConfirmModal').modal('hide');
                });
        }
    };
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
