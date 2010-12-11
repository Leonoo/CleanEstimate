#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from question2_ui import Ui_Question2

class Question2(QtGui.QDialog):
    def __init__(self, model, row, parent=None):
        super(Question2, self).__init__(parent)

        #Private
        self.__ui = Ui_Question2()
        self.__model = model
        self.__mapper = QtGui.QDataWidgetMapper(self)

        self.__ui.setupUi(self)

        #self.__ui.pb_Add.clicked.connect(self.pb_Add_clicked)

        self.__mapper.setModel(model)
        self.__mapper.setSubmitPolicy(QtGui.QDataWidgetMapper.ManualSubmit)
        self.__mapper.setItemDelegate(QtSql.QSqlRelationalDelegate(self.__mapper))

    #def getModel(self):
    #    return self.__model

    #    def clicked(self, index):
    #    print(index)
    #    if self.__NewMode:
    #        self.__NewMode = False
    #        print("revert")
    #        self.__model.revertAll()

    #self.__ui.le_ID.clear()
    #    self.__ui.pb_Objects.setDisabled(False)
    #    self.__mapper.setCurrentIndex(index.row())

    def pb_Add_clicked(self):
        pass

    def newIndex(self, text):
        rowCount = self.__model.rowCount()
        for i in xrange(rowCount):
            print(self.__model.data(self.__model.index(i, 2)))
            if self.__model.data(self.__model.index(i, 2)) == text:
                return i
        return -1