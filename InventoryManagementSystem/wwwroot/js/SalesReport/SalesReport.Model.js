/// <reference path="../knockout.js" />

var SalesMasterVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0); 
    self.SalesDate = ko.observable(item.salesDate || '');
    self.CustomerName = ko.observable(item.customerName || '');
    self.InvoiceNumber = ko.observable(item.invoiceNumber || '');
    self.BillAmount = ko.observable(item.billAmount || '');
    self.Discount = ko.observable(item.discount || '');
    self.NetAmount = ko.observable(item.netAmount || '');
    self.Sales = ko.observableArray((item.sales || []).map(function (item) {
        return new SalesDetailVM(item);
    }))
}

var SalesDetailVM = function (item) {
    var self = this;
    item = item || {};
    self.ItemId = ko.observable(item.itemId || '');
    self.Unit = ko.observable(item.unit || '');
    self.Quantity = ko.observable(item.quantity || '');
    self.Amount = ko.observable(item.amount || '');
    self.Price = ko.observable(item.price || '');
}

var CustomerNameVM = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.CustomerName = ko.observable(item.customerName || '');
}

var ItemNameVM = function(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.ItemName = ko.observable(item.itemName || '');
}