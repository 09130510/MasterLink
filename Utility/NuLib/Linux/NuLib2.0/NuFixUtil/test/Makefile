#
# @(#)Makefile
#
# Modification History:
#

# Makefile for Linux platform
TMPDIR =../tmp
CC = gcc

CCFLAG = -Wall -g -O3 -DDEBUG -DUDEBUG 

INCLUDEDIR = -I. -I../ -I../../include
LIBDIR = -L../tmp/dynamic -L../../bin
LIBS = -lpthread -lm -lNuFixUtil -lNuUtil
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

OBJS = 

all: tOther
	@echo "---- end of build ----"	

tOther: tOther.o
	@echo "---- build $(TMPDIR)/$@ library ----"
	$(LINK) -o $(TMPDIR)/$@ $(TMPDIR)/tOther.o $(LIBDIR) $(LIBS)
	@touch $(TMPDIR)/$@
	@echo .

tSort: tSort.o
	@echo "---- build $(TMPDIR)/$@ library ----"
	$(LINK) -o $(TMPDIR)/$@ $(TMPDIR)/tSort.o $(LIBDIR) $(LIBS)
	@touch $(TMPDIR)/$@
	@echo .

tFix: tFix.o
	@echo "---- build $(TMPDIR)/$@ library ----"
	$(LINK) -o $(TMPDIR)/$@ $(TMPDIR)/tFix.o $(LIBDIR) $(LIBS)
	@touch $(TMPDIR)/$@
	@echo .

.c.o:
	@echo "---- Compile $< ----"
	$(COMPILE) -o $(TMPDIR)/$@ -c $<
	@echo .
.s.o:
	@echo "---- Assemble $< ----"
	$(ASSEMBLE) $<
	@echo .
	
clean:
	@echo "---- clean all object file ----"
	rm -f $(TMPDIR)/tStr* \
 	$(TMPDIR)/tMultiMap* \
 	$(TMPDIR)/tHsLst* 

