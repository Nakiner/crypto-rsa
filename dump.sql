/*
Navicat MySQL Data Transfer

Source Server         : ovl.io
Source Server Version : 50711
Source Host           : ovl.io:3306
Source Database       : crypt

Target Server Type    : MYSQL
Target Server Version : 50711
File Encoding         : 65001

Date: 2016-05-07 17:00:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for bans
-- ----------------------------
DROP TABLE IF EXISTS `bans`;
CREATE TABLE `bans` (
  `date` datetime(6) DEFAULT NULL,
  `ip` varchar(16) DEFAULT NULL,
  `attpt` int(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for private
-- ----------------------------
DROP TABLE IF EXISTS `private`;
CREATE TABLE `private` (
  `keyid` int(20) DEFAULT NULL,
  `login` varchar(32) DEFAULT NULL,
  `value` varchar(1024) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for public
-- ----------------------------
DROP TABLE IF EXISTS `public`;
CREATE TABLE `public` (
  `keyid` int(6) DEFAULT NULL,
  `value` varchar(1024) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(32) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `email` varchar(32) DEFAULT NULL,
  `status` int(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
