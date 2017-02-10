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

INCLUDEDIR = -I. -I../../NuLib2.0/include
LIBDIR = -L../../NuLib2.0/bin
LIBS = -lpthread -lm -lrt -lNuSocket -lNuUtil 
COMPILE = $(CC) $(INCLUDEDIR) $(CCFLAG) 
AS = as
ASFLAG =
ASSEMBLE = $(AS) $(ASFLAG)
LINKER = gcc
LFLAG = -g -O3
LINK = $(LINKER) $(LFLAG) 
AR = ar
AFLAG = ruv

all: $(TMPDIR)/TukanBus $(TMPDIR)/libTukanBusApi.a
	@echo "---- end of build ----"	

# TukanBus
$(TMPDIR)/TukanBus: $(TMPDIR)/TukanBus.o
	@echo "---- build $@ ----"
	$(LINKER) $(LFLAG) -o $@ $^ $(LIBDIR) $(LIBS)
	@echo .

$(TMPDIR)/TukanBus.o: TukanBus.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $@ -c $<
	@echo .

#TukanBusApi
$(TMPDIR)/libTukanBusApi.a: $(TMPDIR)/TukanBusApi.o
	@echo "---- build $@ library ----"
	$(AR) $(AFLAG) $@ $^ 1> /dev/null
	@ranlib $(TMPDIR)/$@
	@echo .

$(TMPDIR)/TukanBusApi.o: TukanBusApi.c
	@echo "---- Compile $< ----"
	$(COMPILE) -o $@ -c $<
	@echo .

clean:
	@echo "---- clean all object file ----"
	rm -f $(TMPDIR)/*.o
	rm -f $(TMPDIR)/TukanBus
	rm -f $(TMPDIR)/libTukanBusApi

install: all
	@echo "Install executables ... $(BIN)" ; \
	echo "Install libraries ... TukanBus, TukanBusApi.a" ; \
	echo "cp $(TMPDIR)/TukanBus $(TMPDIR)/libTukanBusApi.a $(BINDIR)/"; \
	cp $(TMPDIR)/TukanBus $(TMPDIR)/libTukanBusApi.a $(BINDIR)/. ; \
	echo "Install headers ... " ; \
	echo "cp *.h ../include/."; \
	cp *.h ../include/.

