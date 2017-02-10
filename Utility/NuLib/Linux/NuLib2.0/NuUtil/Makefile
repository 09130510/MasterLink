#
# @(#)Makefile
#
# Modification History:
#

# Makefile for Linux platform

TARGET = libNuUtil.a
SOURCE =  NuCommon.o \
		  NuTime.o \
		  NuUtil.o \
          NuFile.o \
          NuFileStream.o \
          NuThread.o \
          NuStr.o \
          NuTools.o \
          NuStream.o \
          NuLog.o \
		  NuMQ.o \
		  NuHangerQ.o \
		  NuHash.o \
		  NuHashMap.o \
		  NuSeqNo.o \
		  NuBuffer.o \
		  NuIni.o \
          NuIPCQ.o \
          NuMMapStream.o \
          NuMMap.o \
          NuCStr.o \
          NuNum.o \
		  NuDaemon.o \
		  NuFMnt.o \
		  NuCBMgr.o \
		  NuTimer.o \
		  NuInvoker.o \
		  NuDLibMgr.o \
		  NuBlocking.o \
		  NuLock.o

DIR = $(shell pwd)
TMPDIR = $(DIR)/tmp/static
PICTMPDIR = $(DIR)/tmp/dynamic
BINDIR = ../bin
HEADERDIR = ../include
CC = gcc
CCFLAG = -Wall -g -O3
PICFLAG =-fpic -ldl
INCLUDEDIR = -I. 
LIBDIR = 
LIBS = -lpthread -lm 
COMPILE = $(CC) $(INCLUDEDIR) $(CCFLAG) $(LIBS)

AR = ar
AFLAG = ruv

.PHONY: all clean

all: init $(TMPDIR)/$(TARGET) $(PICTMPDIR)/$(TARGET) 
	@echo "---- end of build ----"	

init:
	@if [ ! -d $(TMPDIR) ]; \
	then \
		mkdir -p $(TMPDIR); \
	fi
	@if [ ! -d $(PICTMPDIR) ]; \
	then \
		mkdir -p  $(PICTMPDIR); \
	fi

%/$(TARGET): $(SOURCE)
	@echo "---- build $@ library ----"
	cd $*; $(AR) $(AFLAG) $@ $^ 1> /dev/null
	@ranlib $@ 
	@echo .

%.o: %.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $(TMPDIR)/$@ -c $<
	$(COMPILE) $(PICFLAG) -o $(PICTMPDIR)/$@ -c $<
	@echo .

clean:
	@echo "---- clean all object file ----"
	cd $(TMPDIR); rm -f *.o $(TARGET)
	cd $(PICTMPDIR); rm -f *.o $(TARGET)

install: all
	cd $(DIR)
	@echo "Install executables ... $(BINDIR)" ; \
	echo "Install libraries ... $(TARGET)" ; \
	echo "cp $(TMPDIR)/$(TARGET) $(BINDIR)/"; \
	cp $(TMPDIR)/$(TARGET) $(BINDIR)/. ; \
	cp $(PICTMPDIR)/$(TARGET) $(BINDIR)/PIC ; \
	echo "Install headers ... " ; \
	echo "cp *.h $(HEADERDIR)"; \
	cp *.h $(HEADERDIR)

