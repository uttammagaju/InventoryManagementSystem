/// <reference path="../knockout.js" />


var SalesMasterVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.CustomerId = ko.observable(item.customerId || '');
    self.SalesDate = ko.observable(item.salesDate || '');
    self.CustomerName = ko.observable(item.customerName || '');
    self.InvoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.Discount = ko.observable(item.discount || 0);
    self.Sales = ko.observableArray((item.sales || []).map(function (item) {
        return new SalesDetailVM(item);
    }));

    // Computed property for billAmount
    self.BillAmount = ko.computed(() => {
        return self.Sales().reduce((sum, item) => sum + parseFloat(item.Amount() || 0), 0);
    });

    self.CalculatePercentage = ko.computed(() => {
        return (parseFloat(self.Discount() || 0) / 100) * self.BillAmount();
    });

    self.NetAmount = ko.computed(() => {
        return self.BillAmount() - self.CalculatePercentage();
    });
};

var SalesDetailVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.ItemId = ko.observable(item.itemId || '');
    self.Unit = ko.observable(item.unit || '');
    self.Quantity = ko.observable(item.quantity || 0);
    self.Amount = ko.observable(item.amount || 0);
    self.Price = ko.observable(item.price || 0);
    self.Amount = ko.computed(function () {
        return self.Quantity() * self.Price();
    });
};


var CustomerNameVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.CustomerName = ko.observable(item.customerName || '');
};

var ItemNameVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.ItemName = ko.observable(item.itemName || '');
};