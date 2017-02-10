#
# @(#)Makefile
#
# Modification History:
#

# Makefile for Linux platform
TMPDIR = tmp
CC = gcc

CCFLAG = -Wall -g -O3 -DDEBUG -DUDEBUG 
#CCFLAG = -Wall -g -O -DDEBUG -DUDEBUG 

INCLUDEDIR = -I. -I../NuUtil/
#LIBDIR = -L../../bin
LIBDIR = -L../bin
LIBS = -lpthread -lm -lNuUtil #-lrt
COMPILE = $(CC) $(INCLUDEDIR) $(CCFLAG) 
AS = as
ASFLAG =
ASSEMBLE = $(AS) $(ASFLAG)
LINKER = gcc
#LFLAG = -g -O3
LFLAG = -g 
LINK = $(LINKER) $(LFLAG) 
AR = ar
AFLAG = ruv
vpath %.o $(TMPDIR)

OBJS = 

all: init NuIPCQDeQ NuIPCQEnQ
	@echo "---- end of build ----"	

init:
	@if [ ! -d $(TMPDIR) ]; \
	then \
		mkdir $(TMPDIR); \
	fi

NuIPCQDeQ: NuIPCQDeQ.o
	@echo "---- build $(TMPDIR)/$@ library ----"
	$(LINK) -o $(TMPDIR)/$@ $(TMPDIR)/NuIPCQDeQ.o $(LIBDIR) $(LIBS)
	@touch $(TMPDIR)/$@
	@echo .

NuIPCQEnQ: NuIPCQEnQ.o
	@echo "---- build $(TMPDIR)/$@ library ----"
	$(LINK) -o $(TMPDIR)/$@ $(TMPDIR)/NuIPCQEnQ.o $(LIBDIR) $(LIBS)
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
	rm -f $(TMPDIR)/NuIPCQ* 


install: all
	@echo "Install executables ... $(BIN)" ; \
	cp $(TMPDIR)/NuIPCQ* ../bin/. ; \
	echo "cp $(TMPDIR)/NuIPCQ* ../bin/ " ; \

