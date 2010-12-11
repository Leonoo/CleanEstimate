#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from calculationwidget_ui import Ui_CalculationWidget

class CalculationWidget(QtGui.QWidget):
    def __init__(self, objectID, parent=None):
        super(CalculationWidget, self).__init__(parent)

        #Private
        self.__ui = Ui_CalculationWidget()
        self.__model = None
        self.__objectID = objectID

        self.__ui.setupUi(self)

    def setModel(self, model):
        self.__model = model
        self.__ui.tableView.setModel(model)
        self.__ui.tableView.setItemDelegate(QtSql.QSqlRelationalDelegate( self.__ui.tableView))
      
        self.__ui.tableView.setEditTriggers(QtGui.QAbstractItemView.NoEditTriggers);
        self.__ui.tableView.setSelectionMode(QtGui.QAbstractItemView.SingleSelection);
        self.__ui.tableView.setSelectionBehavior(QtGui.QAbstractItemView.SelectRows);

        #self.__ui.tableView.setSortingEnabled(True)
        #self.__ui.tableView.sortByColumn(0,QtCore.Qt.AscendingOrder)
        
        #self.__ui.tableView.clicked.connect(self.clicked)

        #self.__ui.tableView.setColumnHidden(0, True)

        self.__ui.tableView.horizontalHeader().setResizeMode(2, QtGui.QHeaderView.Stretch)
        self.__ui.tableView.horizontalHeader().setDefaultAlignment(QtCore.Qt.AlignLeft)

        rowCount = self.__model.rowCount()
        if rowCount <= 0:
            self.newLine()            

    def getModel(self):
        return self.__model

    def setHR(self, hr):
        self.__hourlyRate = hr

    def getHR(self):
        return self.__hourlyRate

    HourlyRate = QtCore.Property(float, getHR, setHR)

    def setWd(self, wd):
        self.__workdays = wd

    def getWd(self):
        return self.__workdays

    Workdays = QtCore.Property(float, getWd, setWd)

    def newLine(self):
        record = self.__model.record()
        record.setValue(1, self.__objectID)
        record.setValue(3, -1)
        record.setValue(4, -1)
        record.setValue(5, -1)
        record.setValue(9, -1)
        self.__model.insertRecord(-1, record)
        if self.__model.submit():
            print(self.__model.lastError())

    
