# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ui/question2.ui'
#
# Created: Sat Dec 11 23:50:55 2010
#      by: PySide uic UI code generator
#
# WARNING! All changes made in this file will be lost!

from PySide import QtCore, QtGui

class Ui_Question2(object):
    def setupUi(self, Question2):
        Question2.setObjectName("Question2")
        Question2.resize(374, 133)
        self.verticalLayout_3 = QtGui.QVBoxLayout(Question2)
        self.verticalLayout_3.setObjectName("verticalLayout_3")
        self.verticalLayout = QtGui.QVBoxLayout()
        self.verticalLayout.setObjectName("verticalLayout")
        self.l_Up = QtGui.QLabel(Question2)
        self.l_Up.setObjectName("l_Up")
        self.verticalLayout.addWidget(self.l_Up)
        self.le_Value = QtGui.QLineEdit(Question2)
        self.le_Value.setObjectName("le_Value")
        self.verticalLayout.addWidget(self.le_Value)
        self.verticalLayout_3.addLayout(self.verticalLayout)
        self.verticalLayout_2 = QtGui.QVBoxLayout()
        self.verticalLayout_2.setObjectName("verticalLayout_2")
        self.l_Down = QtGui.QLabel(Question2)
        self.l_Down.setObjectName("l_Down")
        self.verticalLayout_2.addWidget(self.l_Down)
        self.le_Description = QtGui.QLineEdit(Question2)
        self.le_Description.setObjectName("le_Description")
        self.verticalLayout_2.addWidget(self.le_Description)
        self.verticalLayout_3.addLayout(self.verticalLayout_2)
        self.buttonBox = QtGui.QDialogButtonBox(Question2)
        self.buttonBox.setOrientation(QtCore.Qt.Horizontal)
        self.buttonBox.setStandardButtons(QtGui.QDialogButtonBox.Cancel|QtGui.QDialogButtonBox.Ok)
        self.buttonBox.setObjectName("buttonBox")
        self.verticalLayout_3.addWidget(self.buttonBox)

        self.retranslateUi(Question2)
        QtCore.QObject.connect(self.buttonBox, QtCore.SIGNAL("accepted()"), Question2.accept)
        QtCore.QObject.connect(self.buttonBox, QtCore.SIGNAL("rejected()"), Question2.reject)
        QtCore.QMetaObject.connectSlotsByName(Question2)

    def retranslateUi(self, Question2):
        Question2.setWindowTitle(QtGui.QApplication.translate("Question2", "Question", None, QtGui.QApplication.UnicodeUTF8))
        self.l_Up.setText(QtGui.QApplication.translate("Question2", "Up", None, QtGui.QApplication.UnicodeUTF8))
        self.l_Down.setText(QtGui.QApplication.translate("Question2", "Down", None, QtGui.QApplication.UnicodeUTF8))


if __name__ == "__main__":
    import sys
    app = QtGui.QApplication(sys.argv)
    Question2 = QtGui.QDialog()
    ui = Ui_Question2()
    ui.setupUi(Question2)
    Question2.show()
    sys.exit(app.exec_())

