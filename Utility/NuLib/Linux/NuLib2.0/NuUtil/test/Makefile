#
# @(#)Makefile
#
# Modification History:
#

# Makefile for Linux platform
TMPDIR =../tmp
CC = gcc

#CCFLAG = -Wall -g -O3 -DDEBUG -DUDEBUG 
CCFLAG = -Wall -g -DDEBUG -DUDEBUG  -rdynamic
DCFLAG = -fpic -g -O3 -Wall -shared
LDFLAG = -shared

INCLUDEDIR = -I. -I../ -I../../include 
#LIBDIR = -L../../bin
LIBDIR = -L../tmp/static -L../../bin
LIBDIR_SO = -L../tmp/dynamic -L../../bin/PIC
#LIBDIR_SO = -L../../bin/PIC
#LIBDIR_SO = -L../tmp/dynamic
LIBS = -lNuFixUtil -lNuUtil -lpthread -lm -lrt -ldl 
SOCOMPILE = $(CC) $(INCLUDEDIR) $(DCFLAG) 

COMPILE = $(CC) $(INCLUDEDIR) $(CCFLAG) 
AS = as
ASFLAG =
ASSEMBLE = $(AS) $(ASFLAG)
LINKER = gcc
#LFLAG = -g -O3
LFLAG = -g -O
LINK = $(LINKER) $(LFLAG) 
AR = ar
AFLAG = ruv

#all: Fun.so Fun2.so tDLib tDLib2 tOther tmstrm tLog tSeqNo tIni
all: tSeqNo tIni tBaseVecIt
	@echo "Target of make should be specified."	

%: %.c
	@echo "---- build Testing program $(TMPDIR)/$@ ----"
	$(COMPILE) -o $(TMPDIR)/$@ $< $(LIBDIR_SO) $(LIBS)
	@touch $(TMPDIR)/$@
	@echo .

%.so: %.c
	@echo "---- build Testing program $(TMPDIR)/$@ ----"
	$(SOCOMPILE) -o $(TMPDIR)/$@ $< $(LIBDIR_SO) $(LIBS)
	@echo .
