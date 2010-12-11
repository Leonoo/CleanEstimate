#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from objectwidget_ui import Ui_ObjectWidget

class ObjectWidget(QtGui.QWidget):
    def __init__(self, customerID, parent=None):
        super(ObjectWidget, self).__init__(parent)

        #Private
        self.__NewMode = False
        self.__ui = Ui_ObjectWidget()
        self.__model = None
        self.__mapper = None
        self.__customerID = customerID

        self.__ui.setupUi(self)

        self.__ui.pb_Apply.clicked.connect(self.pb_Apply_clicked)
        self.__ui.pb_New.clicked.connect(self.pb_New_clicked)
        #self.__ui.pb_Calculation.clicked.connect(self.pb_Objects_clicked)

        self.__mapper = QtGui.QDataWidgetMapper(self)
        self.__mapper.setSubmitPolicy(QtGui.QDataWidgetMapper.ManualSubmit)
        self.__mapper.setItemDelegate(QtSql.QSqlRelationalDelegate(self.__mapper))

    def setModel(self, model):
        self.__model = model
        self.__ui.tableView.setModel(model)
        self.__ui.tableView.setItemDelegate(QtSql.QSqlRelationalDelegate( self.__ui.tableView))
      
        self.__ui.tableView.setEditTriggers(QtGui.QAbstractItemView.NoEditTriggers);
        self.__ui.tableView.setSelectionMode(QtGui.QAbstractItemView.SingleSelection);
        self.__ui.tableView.setSelectionBehavior(QtGui.QAbstractItemView.SelectRows);

        self.__ui.tableView.setSortingEnabled(True)
        self.__ui.tableView.sortByColumn(0,QtCore.Qt.AscendingOrder)
        
        self.__ui.tableView.clicked.connect(self.clicked)

        self.__ui.tableView.setColumnHidden(0, True)
        self.__ui.tableView.setColumnHidden(1, True)

        self.__ui.tableView.setColumnHidden(3, True)
        self.__ui.tableView.setColumnHidden(4, True)
        self.__ui.tableView.setColumnHidden(5, True)
        self.__ui.tableView.setColumnHidden(6, True)
        self.__ui.tableView.setColumnHidden(7, True)
        self.__ui.tableView.setColumnHidden(8, True)
        self.__ui.tableView.setColumnHidden(9, True)
        self.__ui.tableView.setColumnHidden(10, True)

        self.__ui.tableView.horizontalHeader().setResizeMode(2, QtGui.QHeaderView.Stretch)
        self.__ui.tableView.horizontalHeader().setDefaultAlignment(QtCore.Qt.AlignLeft)

        self.__mapper.setModel(model)
        self.__mapper.addMapping(self.__ui.le_Name, 2)
        self.__mapper.addMapping(self.__ui.le_Street, 3)
        self.__mapper.addMapping(self.__ui.le_Number, 4)
        self.__mapper.addMapping(self.__ui.le_Zip, 5)
        self.__mapper.addMapping(self.__ui.le_City, 6)
        self.__mapper.addMapping(self.__ui.te_Note, 7)

    def getModel(self):
        return self.__model
        

    def pb_Apply_clicked(self):
        if len(self.__ui.le_Name.text().strip()) > 0:
            self.__ui.le_Name.setText(self.__ui.le_Name.text().strip(" "))
            if self.__mapper.submit():
                self.__mapper.setCurrentIndex(self.newIndex(self.__ui.le_Name.text()))
                if self.__NewMode:
                    self.__NewMode = False
                    self.__ui.pb_Calculation.setDisabled(False)
            else:
                print(self.__model.lastError())

            if self.__model.lastError().number() != -1:
                print(self.__model.lastError())
                QtGui.QMessageBox.critical(self, self.trUtf8("Fehler!!!"),
                    self.trUtf8("Fehler beim einfügen des Objektes."),
                    QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)
                print("Error")
        else:
            QtGui.QMessageBox.critical(self, self.trUtf8("Fehler!!!"),
            self.trUtf8("Bitte einen Namen angeben."),
            QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)

    def pb_New_clicked(self):
        if not self.__NewMode:
            self.__NewMode = True
            self.__ui.pb_Calculation.setDisabled(True)

            record = self.__model.record()
            record.setValue(1, self.__customerID)
            self.__model.insertRecord(-1, record)
            self.__mapper.setCurrentIndex(self.__model.rowCount() - 1)
            self.__ui.tableView.selectRow(self.__model.rowCount() - 1)

    def clicked(self, index):
        print(index)
        if self.__NewMode:
            self.__NewMode = False
            print("revert")
            self.__model.revertAll()
           
        self.__ui.pb_Calculation.setDisabled(False)
        self.__mapper.setCurrentIndex(index.row())

    def newIndex(self, text):
        rowCount = self.__model.rowCount()
        for i in xrange(rowCount):
            print(self.__model.data(self.__model.index(i, 2)))
            if self.__model.data(self.__model.index(i, 2)) == text:
                return i
        return -1