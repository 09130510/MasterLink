#
# @(#)Makefile
#
# Modification History:
#
#
# Makefile for Linux platform
TMPDIR = ../tmp
BINDIR = ../bin
CC = gcc

CCFLAG = -Wall -g -O3 -DDEBUG -DUDEBUG 

INCLUDEDIR = -I. -I../../NuLib2.0/include -I../src
LIBDIR = -L../tmp -L../../NuLib2.0/bin
LIBS = -lpthread -lm -lrt -lTukanBusApi -lNuSocket -lNuUtil
COMPILE = $(CC) $(INCLUDEDIR) $(CCFLAG) 
AS = as
ASFLAG =
ASSEMBLE = $(AS) $(ASFLAG)
LINKER = gcc
LFLAG = -g -O3
LINK = $(LINKER) $(LFLAG) 
AR = ar
AFLAG = ruv

all: $(TMPDIR)/tBusApi $(TMPDIR)/tBusApi_Short $(TMPDIR)/tBusApi_Sender
	@echo "---- end of build ----"	

# tBusApi
$(TMPDIR)/tBusApi: $(TMPDIR)/tBusApi.o
	@echo "---- build $@ ----"
	$(LINKER) $(LFLAG) -o $@ $^ $(LIBDIR) $(LIBS)
	@echo .

$(TMPDIR)/tBusApi.o: tBusApi.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $@ -c $<
	@echo .

# tBusApi_Short
$(TMPDIR)/tBusApi_Short: $(TMPDIR)/tBusApi_Short.o
	@echo "---- build $@ ----"
	$(LINKER) $(LFLAG) -o $@ $^ $(LIBDIR) $(LIBS)
	@echo .

$(TMPDIR)/tBusApi_Short.o: tBusApi_Short.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $@ -c $<
	@echo .

# tBusApi_Sender
$(TMPDIR)/tBusApi_Sender: $(TMPDIR)/tBusApi_Sender.o
	@echo "---- build $@ ----"
	$(LINKER) $(LFLAG) -o $@ $^ $(LIBDIR) $(LIBS)
	@echo .

$(TMPDIR)/tBusApi_Sender.o: tBusApi_Sender.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $@ -c $<
	@echo .

clean:
	@echo "---- clean all object file ----"
	rm -f $(TMPDIR)/tBusApi.o $(TMPDIR)/tBusApi_Short.o $(TMPDIR)/tBusApi_Sender.o
	rm -f $(TMPDIR)/tBusApi $(TMPDIR)/tBusApi_Short $(TMPDIR)/tBusApi_Sender

