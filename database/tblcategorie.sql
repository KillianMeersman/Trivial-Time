CREATE TABLE `tblcategorie` (
  `categorieID` int(11) NOT NULL,
  `categorieNaam` text,
  PRIMARY KEY (`categorieID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (1,'Geography');
INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (2,'Entertainment');
INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (3,'History');
INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (4,'Arts_and_Literature');
INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (5,'Science');
INSERT INTO `tblcategorie` (`categorieID`,`categorieNaam`) VALUES (6,'Sports');
