/*
SQLyog Professional
MySQL - 5.7.33 : Database - tes_online
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(100) DEFAULT '',
  `password` varchar(255) DEFAULT '',
  `age` int(11) DEFAULT '0',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `user` */

insert  into `user`(`id`,`username`,`password`,`age`,`created_at`,`updated_at`) values 
(1,'yuda@mail.com','$2a$11$tWwo9P4WOWGTE1XShCAGmuaRA/1qvjo8ABV9rFr45OMxgcikYx9PG',18,'2022-12-30 14:37:06','2022-12-30 14:37:06');

/* Procedure structure for procedure `get_user_all` */

/*!50003 DROP PROCEDURE IF EXISTS  `get_user_all` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `get_user_all`()
BEGIN
		select id, username, age, created_at, updated_at from `user`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `get_user_by_email` */

/*!50003 DROP PROCEDURE IF EXISTS  `get_user_by_email` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `get_user_by_email`(
	email varchar(100)
    )
BEGIN
		select id, username, age, created_at, updated_at from `user` where username = email;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `login` */

/*!50003 DROP PROCEDURE IF EXISTS  `login` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `login`(
	email varchar(100)
    )
BEGIN
		select username, `password` from `user` where username=email;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `register` */

/*!50003 DROP PROCEDURE IF EXISTS  `register` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `register`(
	email varchar(100),
	pass varchar(255),
	age int(11)
    )
BEGIN
		insert into `user` (username, `password`, age, created_at, updated_at) values (email, pass, age, now(), now());
	END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
